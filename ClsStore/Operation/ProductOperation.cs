using ClsStore.Entity;
using ClsStore.Service;
using Operation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClsStore.Operation
{
    public class ProductOperation : IProduct
    {
        private IRepository<Product> _product;       
        public ProductOperation(IRepository<Product> product)
        {
            this._product = product;          
        }
        public IQueryable<Product> GetProducts()
        {
            return _product.Table;
        }      

        public Product GetProduct(int id)
        {
            return _product.GetById(id);
        }
        public void InsertPrd(Product product)
        {
            _product.Insert(product);
        }

        public void UpdatePrd(Product product)
        {
            _product.Update(product);
        }

        public void DeletePrd(Product product)
        {
            _product.Delete(product);
        }

        public IEnumerable<Product> GetProductsBySP()
        {
            return _product.GetProductsBySP();
        }
    }
}
