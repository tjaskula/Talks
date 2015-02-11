using System;

namespace Infrastructure.EF
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid ChangePasswordActivationCode { get; set; }
        public DateTime ChangePasswordExpirationTime { get; set; }

        public virtual void SetActivationCode(Guid activationCode, DateTime expiration)
        {
            if (activationCode == Guid.Empty)
                throw new ArgumentException("The activation code is not definied.", "activationCode");

            if (expiration <= DateTime.Now)
                throw new ArgumentException("The expiration date cannot be less then the actual date/time.", "expiration");

            ChangePasswordActivationCode = activationCode;
            ChangePasswordExpirationTime = expiration;
        }
    }
}