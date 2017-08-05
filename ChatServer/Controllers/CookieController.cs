using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class CookieController : ApiController
    {
        CookieHeaderValue cookie;
        const string cookieName = "ChatProject";
        [HttpGet]
        public HttpResponseMessage GetCookie()
        {
            var cookies = Request.Headers.GetCookies();
            if (cookie != null) {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);

        }
        [HttpPost]
        public HttpResponseMessage PostCookie([FromBody]JObject jsonData)
        {
            dynamic json = jsonData;
            string username = json.username;
            string password = json.password;
            string fullname = json.fullname;
            HttpResponseMessage resp;

            cookie = Request.Headers.GetCookies(cookieName).FirstOrDefault();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullname))
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
            if (cookie != null)
                return Request.CreateResponse(HttpStatusCode.Conflict);
            var cookieContent = new NameValueCollection { { "username", username }, { "password", password }, { "fullname", fullname } };
            cookie = new CookieHeaderValue(cookieName, cookieContent);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            //cookie.Path = "/";
            resp = new HttpResponseMessage(HttpStatusCode.Created);
            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;

        }
    }
}
