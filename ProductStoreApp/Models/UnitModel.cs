using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductStoreApp.Models
{
    public class UnitModel
    {
        [DisplayName("Unit ID")]
        public int UnitID { get; set; }
        [Required]
        [DisplayName("Unit")]
        public string UnitType { get; set; }
    }
}