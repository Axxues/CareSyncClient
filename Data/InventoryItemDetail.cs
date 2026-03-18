using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareSync.Data
{
    public class InventoryItemDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Foreign Classes
        [ValidateNever]
        public InventoryStockDetail StockDetail { get; set; }

        [Required(ErrorMessage = "Item Brand Name is required.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Generic Name is required.")]
        public string GenericName { get; set; }

        [Required(ErrorMessage = "Item Category is required.")]
        public string Category { get; set; }


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
