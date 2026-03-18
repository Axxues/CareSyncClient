using CareSync.Data;

namespace CareSync.Common
{
    public class InventoryDashboard
    {
        public int TotalItemsCount { get; set; }
        public int LowStockCount { get; set; }
        public int ExpiringSoonCount { get; set; }

        // This holds the actual table data!
        public PaginatedResult<InventoryStockDetail> InventoryList { get; set; }
    }
}
