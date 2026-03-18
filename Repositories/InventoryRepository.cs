using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using Microsoft.EntityFrameworkCore;

namespace CareSync.Repositories
{
    public class InventoryRepository : BaseRepository<InventoryItemDetail>, IInventoryRepository
    {
        public InventoryRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task AddInventoryItem(InventoryItemDetail itemDetail)
        {
            try
            {
                _dbcontext.Add(itemDetail);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task CreateDispenseInfo(InventoryStockDetail inventoryStockDetail, InventoryDispenseDetail inventoryDispenseDetail)
        {
            if (inventoryStockDetail.InitialQuantity < inventoryDispenseDetail.Quantity)
            {
                throw new Exception("Item to be dispensed should not be grater than stock.");
            }

            using var transaction = await _dbcontext.Database.BeginTransactionAsync();

            try
            {
                inventoryStockDetail.InitialQuantity -= inventoryDispenseDetail.Quantity;
                _dbcontext.Update(inventoryStockDetail);

                _dbcontext.Add(inventoryDispenseDetail);

                await _dbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<IEnumerable<InventoryStockDetail>> GetAllInventory()
        {
            var allItems = await _dbcontext.InventoryStockDetails
            .Include(i => i.ItemDetail)
            .ToListAsync();

            return allItems;
        }

        public async Task<PaginatedResult<InventoryStockDetail>> GetPaginatedInventory(int page, int pageSize, string keyword, string secondaryKeyword)
        {
            var count = await _dbcontext.InventoryStockDetails
                .Where(t => t.ItemDetail.GenericName.Contains(keyword ?? string.Empty))
                .Where(t => t.ItemDetail.Category.Contains(secondaryKeyword ?? string.Empty))
                .CountAsync();

            var records = await _dbcontext.InventoryStockDetails
                .Where(t => t.ItemDetail.GenericName.Contains(keyword ?? string.Empty))
                .Where(t => t.ItemDetail.Category.Contains(secondaryKeyword ?? string.Empty))
                .Include(i => i.ItemDetail)
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 3. Return the formatted result
            return new PaginatedResult<InventoryStockDetail>
            {
                Page = page,
                TotalCount = (int)Math.Ceiling(count / (double)pageSize),
                Result = records,
                TotalRecords = count,
                SearchKeyword = keyword,
                SecondarySearchKeyword = secondaryKeyword
            };
        }

        public async Task<IEnumerable<InventoryDispenseDetail>> GetAllDispenseLog()
        {
            var log = await _dbcontext.InventoryDispenseDetails
            .Include(p => p.PatientInfo)
            .Include(p => p.StockDetail)
                .ThenInclude(c => c.ItemDetail)
            .ToListAsync();

            return log;
        }

        public async Task<PaginatedResult<InventoryDispenseDetail>> GetPaginatedDispenseLog(int page, int pageSize)
        {
            var log = await _dbcontext.InventoryDispenseDetails.CountAsync();

            var records = await _dbcontext.InventoryDispenseDetails
                .Include(p => p.PatientInfo)
                .Include(p => p.StockDetail)
                    .ThenInclude(c => c.ItemDetail)
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 3. Return the formatted result
            return new PaginatedResult<InventoryDispenseDetail>
            {
                Page = page,
                TotalCount = (int)Math.Ceiling(log / (double)pageSize),
                Result = records,
                TotalRecords = log
            };
        }

        public async Task<InventoryStockDetail> GetItemInfo(int id)
        {
            var item = await _dbcontext.InventoryStockDetails
                .Include(p => p.ItemDetail)
                .FirstOrDefaultAsync(p => p.Id == id);

            return item;
        }

        public async Task UpdateItemInfo(int id, InventoryStockDetail inventoryStockDetail)
        {
            try
            {
                var existingItem = await _dbcontext.InventoryStockDetails
                    .Include(p => p.ItemDetail)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingItem == null)
                {
                    throw new KeyNotFoundException("The requested paitemtient profile could not be found.");
                }

                // Map all the changes...
                existingItem.InitialQuantity = inventoryStockDetail.InitialQuantity;
                existingItem.AlertLevel = inventoryStockDetail.AlertLevel;
                existingItem.BatchNumber = inventoryStockDetail.BatchNumber;
                existingItem.ExpiryDate = inventoryStockDetail.ExpiryDate;
                existingItem.UpdatedAt = DateTime.Now;

                if (existingItem.ItemDetail != null && existingItem.ItemDetail != null)
                {
                    existingItem.ItemDetail.ItemName = inventoryStockDetail.ItemDetail.ItemName;
                    existingItem.ItemDetail.GenericName = inventoryStockDetail.ItemDetail.GenericName;
                    existingItem.ItemDetail.Category = inventoryStockDetail.ItemDetail.Category;
                    existingItem.ItemDetail.UpdatedAt = DateTime.Now;
                }

                // Save the changes
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
