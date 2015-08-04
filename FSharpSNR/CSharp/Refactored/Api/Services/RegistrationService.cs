using Domain;
using System;
using Api.Models;
using Api.Mappers;

namespace Api.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ICryptographer _cryptographer;
        private readonly IRegistrationMapper _registrationMapper;

        public RegistrationService(ICryptographer cryptographer,
                                    IRegistrationMapper registrationMapper)
        {
            if (cryptographer == null)
                throw new ArgumentNullException("cryptographer");
            if (registrationMapper == null)
                throw new ArgumentNullException("registrationMapper");

            _cryptographer = cryptographer;
            _registrationMapper = registrationMapper;
        }

        public bool ShouldConfirmSubscription(RegisterRepresentation newRegistration)
        {
            if (newRegistration.Provider == "OAuth") 
                return false;
            return true;
        }

        public bool UserExists(string accountEmail, Func<string, Account> userExistsLookup)
        {
            return !userExistsLookup(accountEmail).Equals(Account.Empty);
        }

        public Account Register(RegisterRepresentation newRegistration)
        {
            newRegistration.Password = _cryptographer.GetPasswordHash(
                                            newRegistration.Password,
                                            _cryptographer.CreateSalt());
            var newAccount = _registrationMapper.Map(newRegistration);
            if (newAccount.Provider == "OAuth")
                newAccount.ConfirmEmail(DateTime.Now);

            return newAccount;
        }

        public bool CanConfirm(Account account, ConfirmationRepresentation representation)
        {
            return account.ActivationCode == representation.Code && account.ActivationCodeExpirationTime >= DateTime.Now;
        }
    }
}