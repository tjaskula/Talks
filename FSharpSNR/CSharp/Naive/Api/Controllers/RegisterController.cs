using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Api.Models;
using Infrastructure.EF;

namespace Api.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpGet]
        public IEnumerable<RegisterInput> Confirmation()
        {
            using (var ctx = new RegistrationContext())
            {
                return ctx.Accounts.Select(a => new RegisterInput {Email = a.Email, Pass = a.Password}).ToList();
            }
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