using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICRUD.Filters;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers.api
{
    public class AuthController : ApiController
    {
        BuisinessService _tokenServices = new BuisinessService();
        private HttpResponseMessage GetAuthToken(string username,string password)
        {
            var token = _tokenServices.GenerateToken(username, password);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", DateTime.Now.AddHours(2).ToString());
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }

        [Route("Authenticating")]
        [HttpGet]

        public HttpResponseMessage Authenticate(string username, string password)
        {
            return GetAuthToken(username, password);
        }
      

        [AuthorizationRequired]
        [Route("GetUserAgain")]
        public string GetUserDetails(string username)
        {
            return "Invalid";
        }
    }
}
