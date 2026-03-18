using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class ConsultationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Key
        [ValidateNever]
        public int PatientPersonalInformationId { get; set; }

        // Navigation Property back to the parent
        [ValidateNever]
        [ForeignKey("PatientPersonalInformationId")]
        public PatientPersonalInformation PatientInfo { get; set; }

        //Foreign Classes
        [ValidateNever]
        public ConsultationPrescription ConsultationPrescription { get; set; }

        //Other Informations

        [Required(ErrorMessage = "Date of Visit is required."), MaxLength(50)]
        public string DateofVisit { get; set; }

        [Required(ErrorMessage = "Blood Pressure is required.")]
        public string BloodPressure { get; set; }

        [Required(ErrorMessage = "Temperature is required.")]
        public float Temperature { get; set; }

        [Required(ErrorMessage = "Heart Rate is required.")]
        public int HeartRate { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public float Weight { get; set; }


        [Display(Name = "Created On")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }
    }
}
