using CareSync.Contracts;
using CareSync.Data;
using CareSync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CareSync.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBaseRepository<PatientPersonalInformation> _ppirepo;
        private readonly IBaseRepository<ConsultationPrescription> _cprepo;
        private readonly IBaseRepository<InventoryStockDetail> _isdrepo;
        private readonly IPatientRepository _cprepoPR;

        public HomeController(
            IBaseRepository<PatientPersonalInformation> ppirepo,
            IBaseRepository<ConsultationPrescription> cprepo,
            IBaseRepository<InventoryStockDetail> isdrepo,
            IPatientRepository cprepoPR
            )
        {
            _ppirepo = ppirepo;
            _cprepo = cprepo;
            _isdrepo = isdrepo;
            _cprepoPR = cprepoPR;
        }

        public IActionResult Index()
        {
            //Checks if the user is logged-in
            //goes to login page if true
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Identity/Account/Login");
            }

            return RedirectToAction("Dashboard");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {

            var allPatients = await _ppirepo.GetAll();
            var allConsultations = await _cprepoPR.GetRecentConsultationsInfo();
            var allMedicines = await _isdrepo.GetAll();

            var today = DateTime.Today;

            var dashboardData = new DashboardViewData
            {
                TotalRegisteredPatients = allPatients.Count(),

                ConsultationsToday = allConsultations.Count(c => c.CreatedAt.Date == today),

                LowStockMedicines = allMedicines.Count(i => i.InitialQuantity <= i.AlertLevel),

                RecentConsultations = allConsultations
            };

            return View(dashboardData);
        }

        public IActionResult Error404()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
