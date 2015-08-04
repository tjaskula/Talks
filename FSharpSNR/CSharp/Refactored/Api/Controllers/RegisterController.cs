using System;
using System.Web.Http;
using Api.Models;
using Api.Services;
using Api.Validators;
using Domain;

namespace Api.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepresentationValidator _validator;
        private readonly IRegistrationService _registrationService;
        private readonly INotifier _notifier;

        public RegisterController(IAccountRepository accountRepository,
                                    IRepresentationValidator validator,
                                    IRegistrationService registrationService,
                                    INotifier notifier)
        {
            if (accountRepository == null)
                throw new ArgumentNullException("accountRepository");
            if (validator == null)
                throw new ArgumentNullException("validator");
            if (registrationService == null)
                throw new ArgumentNullException("validator");
            if (notifier == null)
                throw new ArgumentNullException("notifier");

            _accountRepository = accountRepository;
            _validator = validator;
            _registrationService = registrationService;
            _notifier = notifier;
        }

        [HttpPost]
        [Route("api/confirm")]
        public IHttpActionResult Confirm(ConfirmationRepresentation representation)
        {
            if (ModelState.IsValid)
            {
                var account = _accountRepository.FindByEmail(representation.Email);
                if (_registrationService.CanConfirm(account, representation))
                {
                    account.ConfirmEmail(DateTime.Now);
                    return Ok();
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

            if (registerRepresentation == null)
                ModelState.AddModelError(string.Empty, "The posted body is not valid.");

            if (!_validator.Validate(registerRepresentation))
                ModelState.AddModelError("password", "The password format does not match the policy");

            if (ModelState.IsValid)
            {
                if (!_registrationService.UserExists(registerRepresentation.Email,
                                                        email => _accountRepository.FindByEmail(email)))
                    return Conflict();

                IHttpActionResult response = Ok(); // Here we shoud have set up a Created but would need and URL for created ressource.

                var account = _registrationService.Register(registerRepresentation);

                if (_registrationService.ShouldConfirmSubscription(registerRepresentation))
                {
                    var expirationEndDate = DateTime.Now.AddDays(5);
                    account.SetActivationCode(Guid.NewGuid(), expirationEndDate);
                    _notifier.SendActivationNotification(account.Email);

                    response = Created(confirmationUrl, new ConfirmationRepresentation
                                                                {
                                                                    Email = account.Email,
                                                                    Code = account.ActivationCode,
                                                                    ExpirationTime = account.ActivationCodeExpirationTime.GetValueOrDefault(expirationEndDate)
                                                                });
                }

                _accountRepository.Save(account);

                return response;
            }

            return BadRequest(ModelState);
        }
    }
}