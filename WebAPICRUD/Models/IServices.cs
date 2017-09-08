using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPICRUD.Models
{
    interface IServices
    {
        Login GenerateToken(string username,string password);
        bool ValidateToken(string tokenValue); 
    }
}
