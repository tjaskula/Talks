using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using Api.Models;
using Api.Services;
using Domain;
using Infrastructure.EF;

namespace Api.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpGet]
        public IEnumerable<RegisterRepresentation> Confirmation()
        {
            using (var ctx = new RegistrationContext())
            {
                return ctx.Accounts.Select(a => new RegisterRepresentation { Email = a.Email, Password = a.Password }).ToList();
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post(RegisterRepresentation registerRepresentation)
        {
            var confirmationUrl = new Uri(Request.RequestUri, "/confirmation");

            if (ModelState.IsValid)
            {
                if (!Regex.IsMatch(registerRepresentation.Password,
                    @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$"))
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                using (var ctx = new RegistrationContext())
                {
                    if (ctx.Accounts.Any(a => a.Email == registerRepresentation.Email))
                        return Conflict();

                    var cryptographer = new Cryptographer();
                    string cryptedPassword = cryptographer.GetPasswordHash(
                        registerRepresentation.Password,
                        cryptographer.CreateSalt());

                    var account = new Account
                    {
                        Email = registerRepresentation.Email,
                        Password = cryptedPassword,
                        Provider = registerRepresentation.Provider
                    };

                    if (!account.IsEmailConfirmed)
                    {
                        account.SetActivationCode(Guid.NewGuid(), DateTime.Now.AddDays(5));
                        var notifier = new Notifier();
                        notifier.SendActivaionNotification(account.Email);
                    }

                    ctx.Accounts.Add(account);

                    ctx.SaveChanges();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            return StatusCode(HttpStatusCode.Created);
        }
    }
}