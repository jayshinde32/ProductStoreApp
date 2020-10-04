using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStoreApp.Models
{
    public class ProductModel
    {        
        public int ProductID { get; set; }
        [Required]      
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]\d{0,9}(\.\d{1,2})?%?$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Unit Type")]        
        public int UnitID  { get; set; }
      
        [NotMapped]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public string UnitType { get; set; }
        public CategoryModel Category { get; set; }
        public UnitModel Unit { get; set; }

        [NotMapped]
        [Required]
        public SelectList CategoryList { get; set; }

        [NotMapped]
        [Required]
        public SelectList UnitList { get; set; }

    }
}