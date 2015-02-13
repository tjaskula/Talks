using System;
using System.Collections.Generic;
using System.Data.Entity;
using Domain;

namespace Infrastructure.EF
{
    public class RegistrationInitializer : DropCreateDatabaseIfModelChanges<RegistrationContext>
    {
        protected override void Seed(RegistrationContext context)
        {
            var accounts = new List<Account>
            {
                new Account {Id = 1, Email = "tjaskula1@hotmail.com", Password = "XAABC", Provider = "Manual", ActivationCodeExpirationTime = DateTime.Now},
                new Account {Id = 2, Email = "tjaskula2@hotmail.com", Password = "XAABC", Provider = "Manual", ActivationCodeExpirationTime = DateTime.Now},
                new Account {Id = 3, Email = "tjaskula3@hotmail.com", Password = "XAABC", Provider = "Manual", ActivationCodeExpirationTime = DateTime.Now},
                new Account {Id = 4, Email = "tjaskula4@hotmail.com", Password = "XAABC", Provider = "Manual", ActivationCodeExpirationTime = DateTime.Now},
                new Account {Id = 5, Email = "tjaskula5@hotmail.com", Password = "XAABC", Provider = "Manual", ActivationCodeExpirationTime = DateTime.Now},
            };

            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();
        }
    }
}