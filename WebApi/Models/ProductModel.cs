using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }      
        public string ProductName { get; set; }        
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public int UnitID { get; set; }
        public string UnitType{ get; set; }
        public string CategoryName { get; set; }
        public CategoryModel Category { get; set; }
        public UnitModel Unit { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList UnitList { get; set; }
    }
}