using ClsStore.Entity;
using ClsStore.Service;
using ProductStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProductStoreApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
       
        HttpClient client = new HttpClient();       
        public ActionResult Index()
        {
            IEnumerable<CategoryModel> category = null;

            //client.BaseAddress = new Uri("https://localhost:44323/api/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
           
                //HTTP GET
                var response = client.GetAsync("Category");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CategoryModel>>();
                    readTask.Wait();

                    category = readTask.Result;
                }
                else 
                {                
                    category = Enumerable.Empty<CategoryModel>();
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
                
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
                // client.BaseAddress = new Uri("https://localhost:44323/api/Category");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]+ "Category");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<CategoryModel>("Category", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
         
                 ModelState.AddModelError(string.Empty, "Server Error");

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
            if (id != 0)
            {
                //client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var response = client.GetAsync("Category?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }
            return View(model);
        }
        

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryModel model)
        {
            try
            {
                if(id!=0)
                {
                    //client.BaseAddress = new Uri("https://localhost:44323/api/Category");
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] + "Category");
                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<CategoryModel>("Category", model);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
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
               // client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var response = client.GetAsync("Category?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
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
                   
                    IEnumerable<ProductModel> product = CheckUnitInUse();

                    if (!product.Where(x => x.Category.CategoryID == id).Any())
                    {
                        client = new HttpClient();
                        // client.BaseAddress = new Uri("https://localhost:44323/api/");
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                        //HTTP DELETE
                        var deleteTask = client.DeleteAsync("Category/" + id.ToString());
                        deleteTask.Wait();

                        var result = deleteTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    ModelState.AddModelError("CategoryID", "This category is in use");
                }
                return View("Delete");
            }
            catch
            {
                return View();
            }
        }
        [NonAction]
        private IEnumerable<ProductModel> CheckUnitInUse()
        {
            IEnumerable<ProductModel> product = null;

            // client.BaseAddress = new Uri("https://localhost:44323/api/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            //HTTP GET
            var response = client.GetAsync("Product");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ProductModel>>();
                readTask.Wait();

                product = readTask.Result;

            }
            return product;
        }
    }
}
