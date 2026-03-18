using Microsoft.AspNetCore.Mvc;

namespace CareSync.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FHSIS()
        {
            return View();
        }
    }
}
