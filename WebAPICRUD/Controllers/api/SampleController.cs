using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICRUD.App_Start;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers.api
{
    public class SampleController : ApiController
    {
        [HttpGet]
        [Route("findStudent")]
        public HttpResponseMessage Get(int id)
        {
            Student student = new Student();
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            }
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }


        [HttpGet]
        [Route("CheckStudent")]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = new Student();
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            }
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpGet]
        [Route("CustomStudent")]
        public IHttpActionResult Gets(int id)
        {
            Student student = new Student();
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            }
            if (student == null)
            {
                return new SampleCustomResult("Not Found", Request, HttpStatusCode.NotFound);
            }
           // return Ok(student);
            return new SampleCustomResult(student.FirstName, Request, HttpStatusCode.Found);
        }

    }
}
