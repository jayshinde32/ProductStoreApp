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
    public class CategoryController : Controller
    {
        // GET: Category
        private ICategory _category;
        public CategoryController(ICategory category)
        {
            this._category = category;
        }
        public ActionResult Index()
        {
            IEnumerable<CategoryModel> category = _category.GetCategories().Select(p => new CategoryModel
            {
               CategoryID=p.CategoryID,
               CategoryName=p.CategoryName
            });
            return View(category);
        }

       

        // GET: Category/Create
        public ActionResult Create()
        {
            CategoryModel category = new CategoryModel();
            return View(category);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {
            try
            {
                
                if (model.CategoryID == 0)
                {
                    Category ctEntity = new Category
                    {
                        CategoryName = model.CategoryName,                
                        CreatedDate = DateTime.Now.Date,
                        ModifiedDate = DateTime.Now.Date

                };
                    _category.InsertCat(ctEntity);
                    if (ctEntity.CategoryID > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Category ctEntity = _category.GetCategory(model.CategoryID);
                    ctEntity.CategoryName = model.CategoryName;                   
                    ctEntity.ModifiedDate = DateTime.Now.Date;

                    _category.UpdateCat(ctEntity);
                    if (ctEntity.CategoryID > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View(model);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            CategoryModel model = new CategoryModel();
            if(id!=0)
            {
                Category ctEntity = _category.GetCategory(id);
                model.CategoryName = ctEntity.CategoryName;
            }
            return View(model);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category model)
        {
            try
            {
                if(id!=0)
                {
                    Category ctEntity = _category.GetCategory(id);
                    ctEntity.CategoryName = model.CategoryName;
                    ctEntity.ModifiedDate = DateTime.Now.Date;

                    _category.UpdateCat(ctEntity);
                    if(ctEntity.CategoryID>0)
                    {
                        return RedirectToAction("Index");
                    }
                }
               
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryModel model = new CategoryModel();
            if (id != 0)
            {
                Category ctEntity = _category.GetCategory(id);
                model.CategoryID = ctEntity.CategoryID;
                model.CategoryName = ctEntity.CategoryName;
            }
            return View(model);
           
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    Category ctEntity = _category.GetCategory(id);
                    _category.DeleteCat(ctEntity);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete");
            }
            catch
            {
                return View();
            }
        }
    }
}
