using ClsStore.Entity;
using ClsStore.Service;
using ProductStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private IProduct _product;
        private ICategory _category;

        public ProductController(IProduct product, ICategory category)
        {
            this._product = product;
            this._category = category;
        }
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            
            IEnumerable<ProductModel> product = _product.GetProducts()
                .Select(p => new ProductModel
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryID=p.CategoryID,                  
                    Price = p.Price,
                    Unit = p.Unit,
                });
          
            
            return View(product);
           
        }

        // GET: Product/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {                
                var lst = _category.GetCategories().ToList();

                model.CategoryList = new SelectList(lst.ToList(), "CategoryID", "CategoryName");
                Product prdEntity = _product.GetProduct(id);
                model.ProductID = prdEntity.ProductID;
                model.ProductName = prdEntity.ProductName;
                model.Price = prdEntity.Price;
                model.Unit = prdEntity.Unit;
                model.CategoryID = prdEntity.CategoryID;
            }
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductModel model = new ProductModel();          
            var lst = _category.GetCategories().ToList();
            model.CategoryList = new SelectList(lst.ToList(), "CategoryID", "CategoryName");
            
            return View(model);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            try
            {                
                if (model.ProductID == 0)
                {
                    Product prdEntity = new Product
                    {
                        ProductName = model.ProductName,
                        Price = model.Price,
                        Unit = model.Unit,
                        CategoryID = model.CategoryID,
                        CreatedDate = DateTime.UtcNow.Date,
                        ModifiedDate = DateTime.UtcNow.Date

                    };
                    _product.InsertPrd(prdEntity);
                    if (prdEntity.ProductID > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Product prdEntity = _product.GetProduct(model.ProductID);
                    prdEntity.ProductName = model.ProductName;
                    prdEntity.Price = model.Price;
                    prdEntity.Unit = model.Unit;
                    prdEntity.CategoryID = model.CategoryID;
                    prdEntity.ModifiedDate= DateTime.UtcNow;

                    _product.UpdatePrd(prdEntity);
                    if (prdEntity.ProductID > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View(model);
                
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {
                         
                var lst = _category.GetCategories().ToList();

                model.CategoryList = new SelectList(lst.ToList(), "CategoryID", "CategoryName");
                Product prdEntity = _product.GetProduct(id);
                model.ProductID = prdEntity.ProductID;
                model.ProductName = prdEntity.ProductName;
                model.Price = prdEntity.Price;
                model.Unit = prdEntity.Unit;
                model.CategoryID = prdEntity.CategoryID;
            }
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductModel model)
        {
            try
            {
                if (id != 0)
                {
                    Product prdEntity = _product.GetProduct(id);
                    prdEntity.ProductName = model.ProductName;
                    prdEntity.Price = model.Price;
                    prdEntity.Unit = model.Unit;
                    prdEntity.CategoryID = model.CategoryID;
                    prdEntity.ModifiedDate = DateTime.UtcNow;

                    _product.UpdatePrd(prdEntity);
                    if (prdEntity.ProductID > 0)
                    {
                        return RedirectToAction("index");
                    }
                }

                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {
                Product prdEntity = _product.GetProduct(id);
                model.ProductID = prdEntity.ProductID;
                model.ProductName = prdEntity.ProductName;
                model.Price = prdEntity.Price;
                model.Unit = prdEntity.Unit;
                model.CategoryID = prdEntity.ProductID;
            }
            return View(model);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    Product prdEntity = _product.GetProduct(id);
                    _product.DeletePrd(prdEntity);
                    return RedirectToAction("Index");
                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }
    }
}
