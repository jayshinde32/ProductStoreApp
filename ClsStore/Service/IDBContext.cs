using ClsStore.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Service
{
   public interface IDBContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        IEnumerable<Product> GetProductBySP();
        int SaveChanges();
    }
}
