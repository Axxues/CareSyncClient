using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class InventoryDispenseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [ValidateNever]
        public int PatientPersonalInformationId { get; set; }

        [ValidateNever]
        public int InventoryStockDetailId { get; set; }

        // Navigation Property back to the parent
        [ValidateNever]
        [ForeignKey("PatientPersonalInformationId")]
        public PatientPersonalInformation PatientInfo { get; set; }

        [ValidateNever]
        [ForeignKey("InventoryStockDetailId")]
        public InventoryStockDetail StockDetail { get; set; }

        //Other data

        [Required(ErrorMessage = "Quantity Dispensed is required.")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Date Dispensed is required.")]
        public string DateDispensed { get; set; }

        public string? Instructions { get; set; }

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
