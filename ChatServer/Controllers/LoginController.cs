using ChatLibrary;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServer.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostLogin([FromBody]JObject jsonData)
        {
            dynamic json = jsonData;
            string username = json.username;
            string password = json.password;

            if (new ChatService().contactPassword(username,password))
            {
                Contact test = new ChatService().getContactByUsername(username);
                if (test != null)
                    return Request.CreateResponse(HttpStatusCode.OK, test);
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);

        }
    }
}
