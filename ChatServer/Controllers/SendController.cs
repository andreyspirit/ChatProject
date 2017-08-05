using ChatLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class SendController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostMessage(Message newmsg)
        {
           
            
            if (new ChatService().addNewMessage(newmsg))
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotModified);

        }
    }
}
