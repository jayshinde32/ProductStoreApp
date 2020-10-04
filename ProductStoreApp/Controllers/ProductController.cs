using ClsStore.Entity;
using ClsStore.Service;
using ProductStoreApp.Filter;
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

    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: Product
        [HttpGet]
        public ActionResult Index()
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
            else
            {
                product = Enumerable.Empty<ProductModel>();
                ModelState.AddModelError(string.Empty, "Server error.");
            }

            return View(product);

        }

        // GET: Product/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {

                //client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var response = client.GetAsync("Product?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }

            }
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductModel model = new ProductModel();
            model.CategoryList = new SelectList(BindCategory().ToList(), "CategoryID", "CategoryName");
            model.UnitList = new SelectList(BindUnit().ToList(), "UnitID", "UnitType");

            return View(model);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            try
            {
                // client.BaseAddress = new Uri("https://localhost:44323/api/Product");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] + "Product");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductModel>("Product", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error");

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel();
            IEnumerable<CategoryModel> category = null;
            IEnumerable<UnitModel> unit = null;
            if (id != 0)
            {
                // client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var response = client.GetAsync("Product?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductModel>();
                    readTask.Wait();

                    model = readTask.Result;

                    model.CategoryList = new SelectList(BindCategory().ToList(), "CategoryID", "CategoryName");
                    model.UnitList = new SelectList(BindUnit().ToList(), "UnitID", "UnitType");
                }
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
                    //client.BaseAddress = new Uri("https://localhost:44323/api/Product");
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] + "Product");
                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ProductModel>("Product", model);
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductModel model = new ProductModel();
            if (id != 0)
            {
                // client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var responseTask = client.GetAsync("Product?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }
            return View(model);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Product/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete");
            }
            catch
            {
                return View();
            }
        }

        // Bind categories
        [NonAction]
        private IEnumerable<CategoryModel> BindCategory()
        {
            IEnumerable<CategoryModel> model = null;
            client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44323/api/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            var catResponce = client.GetAsync("Category");
            catResponce.Wait();

            var catResult = catResponce.Result;
            if (catResult.IsSuccessStatusCode)
            {
                var catreadTask = catResult.Content.ReadAsAsync<IList<CategoryModel>>();
                catreadTask.Wait();

                model = catreadTask.Result;
            }
            return model;
        }

        // Bind Unit
        [NonAction]
        private IEnumerable<UnitModel> BindUnit()
        {
            IEnumerable<UnitModel> model = null;
            client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44323/api/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            //HTTP GET
            var respon = client.GetAsync("Unit");
            respon.Wait();

            var res = respon.Result;
            if (res.IsSuccessStatusCode)
            {
                var ureadTask = res.Content.ReadAsAsync<IList<UnitModel>>();
                ureadTask.Wait();
                model = ureadTask.Result;
            }
            return model;
        }

    }
}
