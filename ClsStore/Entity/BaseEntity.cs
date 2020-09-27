using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ClsStore.Entity
{
    public abstract class BaseEntity
    {        
        [Column(null)]
        public DateTime CreatedDate { get; set; }
        [Column(null)]
        public DateTime ModifiedDate { get; set; }
       
    }
}
