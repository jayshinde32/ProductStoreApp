using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductStoreApp.Models
{   
    public class CategoryModel
    {
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }
        [Required]
        [DisplayName("Category")]        
        public string CategoryName { get; set; }

    }
}