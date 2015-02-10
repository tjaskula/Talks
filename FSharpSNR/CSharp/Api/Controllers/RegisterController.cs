using System.Net;
using System.Web.Http;
using Api.Models;

namespace Api.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Confirmation()
        {
            return StatusCode(HttpStatusCode.OK);
        }

        // POST api/<controller>
        public IHttpActionResult Post(RegisterInput registerInput)
        {
            string confirmationUrl = Url.Route("DefaultApi", new { controller = "values", id = "123" });

            if (ModelState.IsValid)
            {
                
            }
            else
            {
                return BadRequest(ModelState);
            }

            return StatusCode(HttpStatusCode.OK);
        }
    }
}