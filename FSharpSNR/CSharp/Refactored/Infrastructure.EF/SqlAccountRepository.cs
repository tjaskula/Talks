using System;
using Domain;

namespace Infrastructure.EF
{
    using System.Linq;

    public class SqlAccountRepository : IAccountRepository, IDisposable
    {
        private readonly RegistrationContext _ctx;

        public SqlAccountRepository(RegistrationContext ctx)
        {
            _ctx = ctx;
        }

        public Account FindByEmail(string email)
        {
            var entity = _ctx.Accounts.SingleOrDefault(a => a.Email == email);

            if (entity == null) return Account.Empty;
            return new Account(
                            entity.Email,
                            entity.Password,
                            entity.Provider,
                            entity.IsEmailConfirmed,
                            entity.ActivationCode,
                            entity.ActivationCodeExpirationTime,
                            entity.ConfirmedOn);
        }

        public void Save(Account account)
        {
            _ctx.Accounts.Add(
                new AccountEntity
                {
                    ActivationCode = account.ActivationCode,
                    ActivationCodeExpirationTime = account.ActivationCodeExpirationTime,
                    ConfirmedOn = account.ConfirmedOn,
                    Email = account.Email,
                    IsEmailConfirmed = account.IsEmailConfirmed,
                    Password = account.Password,
                    Provider = account.Provider
                });

            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        { 
            if (disposing)            
                _ctx.Dispose(); 
        }
    }
}