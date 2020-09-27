using ClsStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Service
{
    public interface IProduct
    {
        IQueryable<Product> GetProducts();
        IEnumerable<Product> GetProductsBySP();
        Product GetProduct(int id);
        void InsertPrd(Product product);
        void UpdatePrd(Product product);
        void DeletePrd(Product product);
    }
}
