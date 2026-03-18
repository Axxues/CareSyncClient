using CareSync.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CareSync.Common
{
    public class ConsultationInfo
    {
        [Required(ErrorMessage = "Patient is required.")]
        public int PatientId { get; set; }

        [ValidateNever]
        public List<PatientPersonalInformation> PatientPersonalInformation { get; set; }

        public ConsultationDetail ConsultationDetail { get; set; }

        public ConsultationPrescription ConsultationPrescription { get; set; }
    }
}
