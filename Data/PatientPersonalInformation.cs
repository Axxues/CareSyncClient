using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class PatientPersonalInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required."), MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required."), MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required."), MaxLength(50)]
        public string DateofBirth { get; set; }

        [Required(ErrorMessage = "Gender is required."), MaxLength(50)]
        public string Gender { get; set; }

        //--------------Contact Information

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Please enter a valid contact number.")]
        public string ContactNumber { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string? SecondaryPhoneNumber { get; set; }

        [Required(ErrorMessage = "Residential Address is required.")]
        [StringLength(100, ErrorMessage = "Residential Address cannot exceed 100 characters.")]
        public string ResidentialAddress { get; set; }

        //Foreign Classes
        [ValidateNever]
        public PatientHealthInformation HealthInformation { get; set; }

        [ValidateNever]
        public PatientEmergencyContact EmergencyContact { get; set; }

        [ValidateNever]
        public List<ConsultationDetail> ConsultationDetail { get; set; }

        [ValidateNever]
        public List<InventoryDispenseDetail> InventoryDispenseDetail { get; set; }

        //--------------Additional Information

        [Required(ErrorMessage = "Blood Type is required.")]
        public string BloodType { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }

        //Gets Age
        [NotMapped] // Tells Entity Framework NOT to try and save this column to the SQL database
        public int Age
        {
            get
            {
                // 1. Try to read the string "1991-10-12" as a real Date
                if (DateTime.TryParse(DateofBirth, out DateTime dob))
                {
                    var today = DateTime.Today;
                    var age = today.Year - dob.Year;

                    // 2. The crucial math: If they haven't had their birthday yet this year, subtract 1!
                    if (dob.Date > today.AddYears(-age))
                    {
                        age--;
                    }

                    return age;
                }

                return 0; // Fallback if the date is invalid or empty
            }
        }
    }
}
