using System;
using System.Linq;
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
        [HttpPost]
        [Route("api/confirm")]
        public IHttpActionResult Confirm(ConfirmationRepresentation representation)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new RegistrationContext())
                {
                    var account =
                        ctx.Accounts.SingleOrDefault(
                            a =>
                            a.Email == representation.Email && a.ActivationCode == representation.Code
                            && a.ActivationCodeExpirationTime >= DateTime.Now);

                    if (account != null)
                    {
                        account.ConfirmEmail(DateTime.Now);
                        ctx.SaveChanges();

                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult Register(RegisterRepresentation registerRepresentation)
        {
            var confirmationUrl = new Uri(Request.RequestUri, "/confirmation");

            if (ModelState.IsValid)
            {
                if (!Regex.IsMatch(registerRepresentation.Password,
                    @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$"))
                {
                    ModelState.AddModelError("password", "The password does not match the policy");
                    return BadRequest(ModelState);
                }

                IHttpActionResult response = Ok(); // Here we shoud have set up a Created but would need and URL for created ressource.

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

                    if (account.Provider == "OAuth")
                        account.ConfirmEmail(DateTime.Now);

                    if (!account.IsEmailConfirmed)
                    {
                        account.SetActivationCode(Guid.NewGuid(), DateTime.Now.AddDays(5));
                        var notifier = new Notifier();
                        notifier.SendActivationNotification(account.Email);

                        response = Created(confirmationUrl, new ConfirmationRepresentation
                        {
                            Email = account.Email,
                            Code = account.ActivationCode,
                            ExpirationTime = account.ActivationCodeExpirationTime.Value
                        });
                    }

                    ctx.Accounts.Add(account);

                    ctx.SaveChanges();

                    return response;
                }
            }

            return BadRequest(ModelState);
        }
    }
}