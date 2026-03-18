using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using CareSync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareSync.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {

        private readonly IBaseRepository<PatientPersonalInformation> _ppirepo;
        private readonly IPatientRepository _ppirepoPR;

        public ConsultationsController(
            IBaseRepository<PatientPersonalInformation> ppirepo,
            IPatientRepository ppirepoPR
            )
        {
            _ppirepo = ppirepo;
            _ppirepoPR = ppirepoPR;
        }

        public async Task<IActionResult> Index(PaginatedRequest request)
        {
            var ppiEntityPR = await _ppirepoPR.GetPaginatedConsultationsInfo(request.PageNumber, PaginatedRequest.ITEMS_PER_PAGE, request.SearchKeyword, request.SecondarySearchKeyword);
            ppiEntityPR.SearchKeyword = request.SearchKeyword;
            ppiEntityPR.SecondarySearchKeyword = request.SecondarySearchKeyword;
            
            return View(ppiEntityPR);
        }

        public async Task<IActionResult> ViewConsultation(int id)
        {
            var ppiEntityPR = await _ppirepoPR.GetConsultationInfo(id);
            return View(ppiEntityPR);
        }

        [Authorize(Roles = "Admin, Attending")]
        public async Task<IActionResult> Create()
        {
            var ppiEntity = (await _ppirepo.GetAll()).ToList();
            return View(new ConsultationInfo()
            {
                PatientPersonalInformation = ppiEntity
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConsultations(ConsultationInfo consultationInfo)
        {
            var ppiEntity = (await _ppirepo.GetAll()).ToList();
            if (!ModelState.IsValid)
            {
                TempData["Result"] = $"Validate";
                return View("Create", new ConsultationInfo()
                {
                    PatientId = consultationInfo.PatientId,
                    PatientPersonalInformation = ppiEntity,
                    ConsultationDetail = consultationInfo.ConsultationDetail,
                    ConsultationPrescription = consultationInfo.ConsultationPrescription
                });
            }

            try
            {
                await _ppirepoPR.CreateConsultationInfo(consultationInfo.PatientId, consultationInfo.ConsultationDetail, consultationInfo.ConsultationPrescription);

            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Error";
                TempData["ErrorMessage"] = ex.Message;
                return View("Create", new ConsultationInfo()
                {
                    PatientId = consultationInfo.PatientId,
                    PatientPersonalInformation = ppiEntity,
                    ConsultationDetail = consultationInfo.ConsultationDetail,
                    ConsultationPrescription = consultationInfo.ConsultationPrescription
                });
            }

            TempData["Result"] = $"Success";
            return RedirectToAction("Create");
        }
    }
}
