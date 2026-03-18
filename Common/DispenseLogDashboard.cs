using CareSync.Data;

namespace CareSync.Common
{
    public class DispenseLogDashboard
    {
        public PaginatedResult<InventoryDispenseDetail> InventoryDispenseDetail { get; set; } = new PaginatedResult<InventoryDispenseDetail>();
        public int TotalDispensedToday { get; set; }
        public int PatientsServedToday { get; set; }
    }
}
