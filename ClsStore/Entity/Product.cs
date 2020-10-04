using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Entity
{
    [Table("tblProduct")]
    public class Product : BaseEntity
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }            
        public int CategoryID { get; set; }
        public int UnitID { get; set; }
        public Category category { get; set; }
        public Unit unit { get; set; }
    }
}
