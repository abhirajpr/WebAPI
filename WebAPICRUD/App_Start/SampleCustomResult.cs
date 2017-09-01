using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPICRUD.App_Start
{
    public class SampleCustomResult : IHttpActionResult
    {
        string _value;
        HttpStatusCode _code;
        HttpRequestMessage _request;

        public SampleCustomResult(string value,HttpRequestMessage request,HttpStatusCode code)
        {
            _value = value;
            _request = request;
            _code = code;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var responce = new HttpResponseMessage()
            {
                Content = new StringContent(_value),
                RequestMessage = _request,
                StatusCode = _code


            };
            return Task.FromResult(responce);
        }
    }
}