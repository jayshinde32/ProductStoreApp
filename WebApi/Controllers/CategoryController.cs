using ClsStore.Entity;
using ClsStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        // GET: api/Category
        private ICategory _category;

        public CategoryController(ICategory category)
        {
            this._category = category;
        }
        public IHttpActionResult GetAllCategories()
        {
            IEnumerable<CategoryModel> category = _category.GetCategories()
               .Select(c => new CategoryModel
               {
                  CategoryID=c.CategoryID,
                  CategoryName=c.CategoryName
               }).ToList();

            if (category.Count() == 0)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // GET: api/Category/5
        public IHttpActionResult Get(int id)
        {
            CategoryModel model = new CategoryModel();
            if (id != 0)
            {
                Category cnEntity = _category.GetCategory(id);
                model.CategoryID = cnEntity.CategoryID;
                model.CategoryName = cnEntity.CategoryName;
            }
            if (model.CategoryID == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST: api/Category
        public IHttpActionResult Post(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    CategoryName = model.CategoryName,
                    CreatedDate = DateTime.Now.Date,
                    ModifiedDate = DateTime.Now.Date

                };
                _category.InsertCat(category);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");


        }

        // PUT: api/Category/5
        public IHttpActionResult Put(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = _category.GetCategory(model.CategoryID);
                category.CategoryName = model.CategoryName;
                category.CreatedDate = DateTime.Now.Date;
                category.ModifiedDate = DateTime.Now.Date;

                _category.UpdateCat(category);
                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(int id)
        {
            if (id != 0)
            {
                Category category = _category.GetCategory(id);
                _category.DeleteCat(category);

                return Ok();
            }
            else
                return BadRequest("Invalid data.");
        }
    }
}
