using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class ConsultationPrescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //PrimaryKey
        [ValidateNever]
        public int ConsultationDetailId { get; set; }

        // Navigation Property back to the parent
        [ValidateNever]
        [ForeignKey("CosultationDetailId")]
        public ConsultationDetail ConsultationDetail { get; set; }

        //Other Informations

        [Required(ErrorMessage = "The Patient Chief Complaint must be recorded.")]
        public string Complaint { get; set; }

        [Required(ErrorMessage = "The Diagnosis must be recorded.")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "The Medications must be recorded.")]
        public string Medications { get; set; }

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
