using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class PatientHealthInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //public int PatientPersonalInformationId { get; set; }

        //PrimaryKey
        [ValidateNever]
        public int PatientPersonalInformationId { get; set; }

        // Navigation Property back to the parent
        [ValidateNever]
        public PatientPersonalInformation PatientInfo { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        public float Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public float Weight { get; set; }

        //[Required(ErrorMessage = "Known Allergies is required.")]
        public string? KnownAllergies { get; set; }

        [Required(ErrorMessage = "The existing medical conditions must be recorded.")]
        public string MedicalCondition { get; set; }


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
