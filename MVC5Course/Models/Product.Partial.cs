namespace MVC5Course.Models
{
    using DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new ValidationResult("台灣只能有一個",new string[] { "ProductName","Stock"});
            //throw new NotImplementedException();
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [ValidateTaiwanAddressAttribute]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool isDeleted { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
