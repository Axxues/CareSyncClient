using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using CareSync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Threading.Tasks;


namespace CareSync.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {

        private readonly IBaseRepository<PatientPersonalInformation> _ppirepo;
        private readonly IPatientRepository _ppirepoPR;

        public PatientsController(
            IBaseRepository<PatientPersonalInformation> ppirepo,
            IPatientRepository ppirepoPR)
        {
            _ppirepo = ppirepo;
            _ppirepoPR = ppirepoPR;
        }

        public async Task<IActionResult> Index(PaginatedRequest request)
        {
            var patients = await _ppirepoPR.GetPaginatedPatient(request.PageNumber, PaginatedRequest.ITEMS_PER_PAGE, request.SearchKeyword, request.SecondarySearchKeyword);
            patients.SearchKeyword = request.SearchKeyword;
            patients.SecondarySearchKeyword = request.SecondarySearchKeyword;
            return View(patients);
        }

        public async Task<IActionResult> ViewPatient(int Id)
        {
            PatientPersonalInformation ppiEntity = await _ppirepoPR.GetPatientInfo(Id);

            return View(ppiEntity);
        }

        public IActionResult Create()
        {
            return View(new PatientProfile());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatientProfileInfo(PatientProfile patientProfile)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Create", patientProfile);
            }

            try
            {
                await _ppirepoPR.CreatePatientProfile(patientProfile.PersonalInformation, patientProfile.HealthInformation, patientProfile.EmergencyContact);

            }
            catch(Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;
                return View("Create", patientProfile);
            }


            TempData["Result"] = $"Success";
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Edit(int id)
        {

            var ppEntity = await _ppirepoPR.GetPatientInfo(id);
            var peEntity = 2;
            if (ppEntity == null)
            {
                return NotFound();
            }

            return View(ppEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPatientProfileInfo(PatientPersonalInformation patientPersonalInformation)
        {
            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Edit", patientPersonalInformation);
            }

            try
            {
                await _ppirepoPR.UpdatePatientInfo(patientPersonalInformation.Id, patientPersonalInformation);
                TempData["Result"] = $"Success";
                return View("Edit", patientPersonalInformation);

            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;
                return View("Edit", patientPersonalInformation);
            }

            
        }
    }
}
