using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.Filter;

namespace WebApi.Controllers
{
    
    public class ProductController : ApiController
    {
        // GET: api/Product
        private IProduct _product;
        private ICategory _category;
        private IUnit _unit;

        public ProductController(IProduct product,ICategory category,IUnit unit)
        {
            this._product = product;
            this._category = category;
            this._unit = unit;
        }
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {           
            IEnumerable<ProductModel> product = _product.GetProducts()
                .Select(p => new ProductModel
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryID = p.CategoryID,
                    Price = p.Price,
                    UnitID = p.UnitID,
                    Category = new CategoryModel
                    {
                        CategoryID = p.category.CategoryID,
                        CategoryName = p.category.CategoryName
                    },
                    Unit = new UnitModel
                    {
                        UnitID = p.unit.UnitID,
                        UnitType = p.unit.UnitType
                    }

                }).ToList();

            if (product.Count() == 0)
            {
                return NotFound();
            }
            return Ok(product);
        }
      

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {
                Product prdEntity = _product.GetProduct(id);
                model.ProductID = prdEntity.ProductID;
                model.ProductName = prdEntity.ProductName;
                model.Price = prdEntity.Price;
                model.UnitID = prdEntity.UnitID;
                model.CategoryID = prdEntity.CategoryID;

                model.CategoryName = _category.GetCategory(model.CategoryID).CategoryName;
                model.UnitType = _unit.GetUnit(model.UnitID).UnitType;
            }
            if (model.CategoryID == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST: api/Product
        public IHttpActionResult Post(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                Product prdEntity = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    UnitID = model.UnitID,
                    CategoryID = model.CategoryID,
                    CreatedDate = DateTime.UtcNow.Date,
                    ModifiedDate = DateTime.UtcNow.Date

                };
                _product.InsertPrd(prdEntity);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");


        }

        // PUT: api/Product/5
        public IHttpActionResult Put(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                Product prdEntity = _product.GetProduct(model.ProductID);
                prdEntity.ProductName = model.ProductName;
                prdEntity.Price = model.Price;
                prdEntity.UnitID = model.UnitID;
                prdEntity.CategoryID = model.CategoryID;
                prdEntity.ModifiedDate = DateTime.UtcNow;

                _product.UpdatePrd(prdEntity);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            if (id != 0)
            {
                Product prdEntity = _product.GetProduct(id);
                _product.DeletePrd(prdEntity);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }
    }
}
