using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class CreateNewVM : IValidatableObject
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請填寫商品名稱")]
        [StringLength(20, ErrorMessage = "商品名稱不得超過20個字元")]

        public string ProductName { get; set; }

        [Required]
        public Nullable<decimal> Price { get; set; }


        public int OrderLineCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!this.ProductName.Contains("台灣"))
            {
                yield return new ValidationResult(
                     "台灣只能有一個",
                     new string[] { "ProductName" });
            }
        }
    }
}