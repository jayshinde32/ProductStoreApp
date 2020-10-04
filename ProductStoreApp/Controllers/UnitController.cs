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
    public class UnitController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: Unit
        public ActionResult Index()
        {
            IEnumerable<UnitModel> unit = null;

            //client.BaseAddress = new Uri("https://localhost:44323//api/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            //HTTP GET
            var response = client.GetAsync("Unit");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<UnitModel>>();
                readTask.Wait();

                unit = readTask.Result;
            }
            else
            {
                unit = Enumerable.Empty<UnitModel>();
                ModelState.AddModelError(string.Empty, "Server error.");
            }
            return View(unit);
        }  
        // GET: Unit/Create
        public ActionResult Create()
        {
            UnitModel model = new UnitModel();
            return View(model);
        }

        // POST: Unit/Create
        [HttpPost]
        public ActionResult Create(UnitModel model)
        {
            try
            {
                //client.BaseAddress = new Uri("https://localhost:44323/api/Unit");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP POST
                var postTask = client.PostAsJsonAsync<UnitModel>("Unit", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error.");
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int id)
        {
            UnitModel model = new UnitModel();

            if(id!=0)
            {
                //client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                var response = client.GetAsync("Unit?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UnitModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }

            }
            return View(model);
        }

        // POST: Unit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,UnitModel model)
        {
            try
            {
                if (id != 0)
                {
                    //client.BaseAddress = new Uri("https://localhost:44323/api/Unit");
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<UnitModel>("Unit", model);
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

        // GET: Unit/Delete/5
        public ActionResult Delete(int id)
        {
            UnitModel model = new UnitModel();
            if (id != 0)
            {
                //client.BaseAddress = new Uri("https://localhost:44323/api/");
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                //HTTP GET
                var response = client.GetAsync("Unit?id=" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UnitModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }               
            }
            return View(model);
        }

        // POST: Unit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    IEnumerable<ProductModel> product = CheckUnitInUse();
                    
                    if (!product.Where(x => x.Unit.UnitID ==id).Any())
                    {
                        client = new HttpClient();
                        // client.BaseAddress = new Uri("https://localhost:44323/api/");
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                        //HTTP DELETE
                        var deleteTask = client.DeleteAsync("Unit/" + id.ToString());
                        deleteTask.Wait();

                        var result = deleteTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    ModelState.AddModelError("UnitType", "This unit type is in use");
                   
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
