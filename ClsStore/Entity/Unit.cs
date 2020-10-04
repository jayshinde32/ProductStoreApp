using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Entity
{
    [Table("tblUnit")]
    public class Unit :BaseEntity
    {
        [Key]
        public int UnitID { get; set; }
        public string UnitType { get; set; }
    }
}
