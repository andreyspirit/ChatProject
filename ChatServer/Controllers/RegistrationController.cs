using ChatLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class RegistrationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostRegistration([FromBody]JObject jsonData)
        {
            dynamic json = jsonData;
            string username = json.username;
            string password = json.password;
            string fullname = json.fullname;

            Contact newContact = new Contact() { Username = username, Name = fullname, Password = password };
            if (new ChatService().contactExists(newContact))
                return Request.CreateResponse(HttpStatusCode.Found);

            new ChatService().addNewContact(newContact);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
