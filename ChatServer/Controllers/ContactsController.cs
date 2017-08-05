using ChatLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class ContactsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetContacts()
        {
            IList<Contact> result = new ChatService().getContacts();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
    