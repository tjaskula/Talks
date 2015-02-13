using Api.Models;
using Domain;
using System;

namespace Api.Services
{
    public interface IRegistrationService
    {
        bool ShouldConfirmSubscription(RegisterRepresentation newRegistration);
        bool CanRegister(string accountEmail, Func<string, Account> canRegister);
        Account Register(RegisterRepresentation newRegistration);
        bool CanConfirm(Account account, ConfirmationRepresentation representation);
    }
}