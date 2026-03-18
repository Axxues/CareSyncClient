
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class InventoryStockDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [ValidateNever]
        public int InventoryItemDetailId { get; set; }

        // Navigation Property back to the parent
        [ValidateNever]
        [ForeignKey("InventoryItemDetailId")]
        public InventoryItemDetail ItemDetail { get; set; }

        [Required(ErrorMessage = "Initial Quantity is required.")]
        public int InitialQuantity { get; set; }

        [Required(ErrorMessage = "Alert Level is required.")]
        public int AlertLevel { get; set; }

        [Required(ErrorMessage = "Batch Number is required.")]
        public string BatchNumber { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        public string ExpiryDate { get; set; }

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
