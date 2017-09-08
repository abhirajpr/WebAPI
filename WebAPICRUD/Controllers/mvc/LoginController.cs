using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers.mvc
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            IEnumerable<Login> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Login");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Login>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<Login>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }
        //Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Login student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Login", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }
    }
}