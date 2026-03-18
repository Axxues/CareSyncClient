using CareSync.Data;

namespace CareSync.Models
{
    public class DashboardViewData
    {
        public int TotalRegisteredPatients { get; set; }

        public int ConsultationsToday { get; set; }

        public int LowStockMedicines { get; set; }

        public IEnumerable<ConsultationDetail> RecentConsultations { get; set; }
    }
}
