using CareSync.Common;
using CareSync.Data;

namespace CareSync.Contracts
{
    public interface IInventoryRepository : IBaseRepository<InventoryItemDetail>
    {
        Task AddInventoryItem(InventoryItemDetail itemDetail);

        Task CreateDispenseInfo(InventoryStockDetail inventoryStockDetail, InventoryDispenseDetail inventoryDispenseDetail);

        Task<IEnumerable<InventoryStockDetail>> GetAllInventory();

        Task<PaginatedResult<InventoryStockDetail>> GetPaginatedInventory(int page, int pageSize, string keyword, string secondaryKeyword);

        Task<IEnumerable<InventoryDispenseDetail>> GetAllDispenseLog();

        Task<PaginatedResult<InventoryDispenseDetail>> GetPaginatedDispenseLog(int page, int pageSize);

        Task<InventoryStockDetail> GetItemInfo(int id);

        Task UpdateItemInfo(int id, InventoryStockDetail inventoryStockDetail);
    }
}
