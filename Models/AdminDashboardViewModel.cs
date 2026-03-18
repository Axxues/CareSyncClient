using CareSync.Common;

namespace CareSync.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalPendingAction { get; set; }
        public int TotalAccounts { get; set; }
        public int TotalActiveStaff { get; set; }

        public PaginatedResult<PendingUserViewModel> PaginatedPendingUsers { get; set; }
    }
}
