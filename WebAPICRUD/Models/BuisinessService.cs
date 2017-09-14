using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPICRUD.Filters;

namespace WebAPICRUD.Models
{
    public class BuisinessService : IServices
    {
        public Login GenerateToken(string username, string password)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issue = DateTime.Now;
            DateTime expire = DateTime.Now.AddMinutes(10);
            var tokendomain = new Login
            {
                Username = username,
                AuthToken = token,
                IssedOn=issue,
                ExpireOn=expire

            };
            Login tk = new Login();
            DBFirstEntities db = new DBFirstEntities();
            tk = db.Logins.Where(x => x.Username == username & x.Password == password).FirstOrDefault();
            if (tk.Username != null)
            {
                tk.AuthToken = tokendomain.AuthToken;
                tk.IssedOn = tokendomain.IssedOn;
                tk.ExpireOn = tokendomain.ExpireOn;
                db.SaveChanges();
            }
            return tokendomain;
        }

        public bool ValidateToken(string tokenValue)
        {
            
            DBFirstEntities db = new DBFirstEntities();
            Login user = new Login();
            user = db.Logins.Where(u => u.AuthToken == tokenValue && u.ExpireOn > DateTime.Now).FirstOrDefault();
            if (user != null)
            {
                user.ExpireOn = DateTime.Now.AddMinutes(5);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    

    }
}