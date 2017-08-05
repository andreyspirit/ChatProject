using ChatLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class ContactController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetContacts()
        {
            IList<Contact> dibila = new ChatService().getContacts();
            return Request.CreateResponse(HttpStatusCode.OK,dibila);
        }
    }
}
