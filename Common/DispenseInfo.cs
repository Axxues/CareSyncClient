using CareSync.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CareSync.Common
{
    public class DispenseInfo
    {
        [Required(ErrorMessage = "Patient is required.")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Medicine / Item Used is required.")]
        public int ItemId { get; set; }

        [ValidateNever]
        public List<PatientPersonalInformation> PatientPersonalInformation { get; set; }

        [ValidateNever]
        public List<InventoryStockDetail> InventoryStockDetail { get; set; }

        public InventoryDispenseDetail InventoryDispenseDetail { get; set; }
    }
}
