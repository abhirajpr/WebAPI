using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers.api
{
    public class LoginController : ApiController
    {
        public IHttpActionResult GetAllStudents()
        {
            List<Models.Login> students = new List<Login>();
            using (var context = new DBFirstEntities())
            {
                students = context.Logins.ToList();
            }

            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found");
        }

        public IHttpActionResult GetStudentById(int id)
        {
            Login student = new Login();
            using (var context = new DBFirstEntities())
            {
                student = context.Logins.Where(s => s.Id == id).FirstOrDefault();
            }
            if (student == null)
                return Ok("No student with given ID found");
            else
                return Ok(student);
        }
        public IHttpActionResult GetStudentsByName(string firstName, string lastName)
        {
            List<Login> students = new List<Login>();
            using (var context = new DBFirstEntities())
            {
                students = context.Logins.Where(s => s.Username == firstName & s.Password == lastName).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found with the given name");
        }

       

        public IHttpActionResult PostNewStudent(Login student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid student.");

            using (var context = new DBFirstEntities())
            {
                context.Logins.Add(new Login()
                {
                    Id = student.Id,
                    Username = student.Username,
                    Password = student.Password,
 
                });
                context.SaveChanges();
            }
            return Ok("New Student Added");
        }
    }
}
