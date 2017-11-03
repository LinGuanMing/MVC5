using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class CreateNewVM
    {
        [Required(ErrorMessage = "請填寫商品名稱")]
        [StringLength(20, ErrorMessage = "商品名稱不得超過20個字元")]
        public string ProductName { get; set; }

        [Required]
        public Nullable<decimal> Price { get; set; }
    }
}