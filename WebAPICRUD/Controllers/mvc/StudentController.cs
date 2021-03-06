﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers.mvc
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<Student> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Student>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<Student>();

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
        public ActionResult Create(Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("student", student);
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
        public ActionResult Details(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }

            return View(student);
        }
        public ActionResult Edit(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }

            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("student", student);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52059/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("student/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}