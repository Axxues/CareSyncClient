using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class PatientEmergencyContact
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

        [Required(ErrorMessage = "Contact Person Name is required."), MaxLength(50)]
        public string ContactPersonName { get; set; }

        [Required(ErrorMessage = "Emergency number is required.")]
        [Phone(ErrorMessage = "Please enter a valid emergency number.")]
        public string EmergencyNumber { get; set; }


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
