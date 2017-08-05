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
    public class MessagesController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]JObject details)
        {
            List<Message> result;
            dynamic input = details;

            result = new ChatService().getMessagesBySenderReceiver(input.sender, input.receiver);
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        [HttpPut]
        public HttpResponseMessage Put(Message new_message)
        {
            new ChatService().addNewMessage(new_message);
            return Request.CreateResponse(HttpStatusCode.OK, new_message);
        }
    }
}
