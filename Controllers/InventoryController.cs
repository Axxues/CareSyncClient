using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using CareSync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSync.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {

        private readonly IInventoryRepository _iidrepoIR;
        private readonly IBaseRepository<PatientPersonalInformation> _ppirepo;
        private readonly IBaseRepository<InventoryItemDetail> _iidrepo;
        private readonly IBaseRepository<InventoryStockDetail> _isdrepo;

        public InventoryController(
            IInventoryRepository iidrepoIR,
            IBaseRepository<PatientPersonalInformation> ppirepo,
            IBaseRepository<InventoryItemDetail> iidrepo,
            IBaseRepository<InventoryStockDetail> isdrepo
            )
        {
            _iidrepoIR = iidrepoIR;
            _ppirepo = ppirepo;
            _iidrepo = iidrepo;
            _isdrepo = isdrepo;
        }

        public async Task<IActionResult> Index(PaginatedRequest request)
        {
            var isdEntity = await _iidrepoIR.GetAllInventory();

            var isdEntityPR = await _iidrepoIR.GetPaginatedInventory(request.PageNumber, PaginatedRequest.ITEMS_PER_PAGE, request.SearchKeyword, request.SecondarySearchKeyword);

            var today = DateTime.Now.Date;
            var thirtyDaysFromNow = today.AddDays(30);

            var dashboardData = new InventoryDashboard()
            {
                InventoryList = isdEntityPR,
                TotalItemsCount = isdEntityPR.TotalRecords,
                LowStockCount = isdEntity.Count(i => i.InitialQuantity <= i.AlertLevel),

                ExpiringSoonCount = isdEntity.Count(i =>
                {
                    if (DateTime.TryParse(i.ExpiryDate, out DateTime parsedDate))
                    {
                        return parsedDate >= today && parsedDate <= thirtyDaysFromNow;
                    }

                    return false;
                })
            };

            return View(dashboardData);
        }

        [Authorize(Roles = "Admin, Encoder")]
        public IActionResult Create()
        {
            return View(new InventoryItemDetail());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(InventoryItemDetail inventoryItemDetail)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Create", inventoryItemDetail);
            }

            try
            {
                await _iidrepoIR.AddInventoryItem(inventoryItemDetail);

            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;
                
                return View("Create", inventoryItemDetail);
            }


            TempData["Result"] = $"Success";
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Admin, Encoder")]
        public async Task<IActionResult> Edit(int id)
        {

            var isdEntityIR = await _iidrepoIR.GetItemInfo(id);

            return View(isdEntityIR);
        }

        public async Task<IActionResult> EditInventoryItem(InventoryStockDetail inventoryStockDetail)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Edit", inventoryStockDetail);
            }

            try
            {
                await _iidrepoIR.UpdateItemInfo(inventoryStockDetail.Id, inventoryStockDetail);

            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;

                return View("Edit", inventoryStockDetail);
            }

            TempData["Result"] = $"Success";
            return  View("Edit", inventoryStockDetail);
        }

        public async Task<IActionResult> Dispense()
        {
            var ppiEntity = (await _ppirepo.GetAll()).ToList();
            var iidEntityIR = (await _iidrepoIR.GetAllInventory()).ToList();
            return View(new DispenseInfo()
            {
                PatientPersonalInformation = ppiEntity,
                InventoryStockDetail = iidEntityIR
            });
        }

        public async Task<IActionResult> CreateDispenseItem(InventoryDispenseDetail inventoryDispenseDetail)
        {
            var ppiEntity = (await _ppirepo.GetAll()).ToList();
            var iidEntityIR = (await _iidrepoIR.GetAllInventory()).ToList();

            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Dispense", new DispenseInfo()
                {
                    PatientPersonalInformation = ppiEntity,
                    InventoryStockDetail = iidEntityIR,
                    InventoryDispenseDetail = inventoryDispenseDetail
                });
            }

            var isdEntity = await _isdrepo.GetOne(inventoryDispenseDetail.InventoryStockDetailId);

            try
            {

                await _iidrepoIR.CreateDispenseInfo(isdEntity, inventoryDispenseDetail);
            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;

                return View("Dispense", new DispenseInfo()
                {
                    PatientPersonalInformation = ppiEntity,
                    InventoryStockDetail = iidEntityIR,
                    InventoryDispenseDetail = inventoryDispenseDetail
                });
            }

            TempData["Result"] = $"Success";
            return View("Dispense", new DispenseInfo()
            {
                PatientPersonalInformation = ppiEntity,
                InventoryStockDetail = iidEntityIR,
                InventoryDispenseDetail = inventoryDispenseDetail
            });
        }

        public async Task<IActionResult> DispenseLog(PaginatedRequest result)
        {
            var iidEntityID = await _iidrepoIR.GetAllDispenseLog();

            var iidEntityPR = await _iidrepoIR.GetPaginatedDispenseLog(result.PageNumber, PaginatedRequest.ITEMS_PER_PAGE);

            var today = DateTime.Now.Date;

            // 2. Map data to your Dashboard ViewModel
            var dashboardData = new DispenseLogDashboard()
            {
                InventoryDispenseDetail = iidEntityPR,

                // Sum the quantities dispensed today
                TotalDispensedToday = iidEntityID.Count(d =>
                {
                    if (DateTime.TryParse(d.DateDispensed, out DateTime parsedDate))
                    {
                        return parsedDate.Date == today;
                    }
                    return false;
                }),

                PatientsServedToday = iidEntityID
                .Where(d =>
                {
                    if (DateTime.TryParse(d.DateDispensed, out DateTime parsedDate))
                    {
                        return parsedDate.Date == today;
                    }
                    return false;
                })
                .Select(d => d.PatientPersonalInformationId)
                .Distinct()
                .Count()
            };

            return View(dashboardData);

        }
    }
}
