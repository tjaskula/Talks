using System;

namespace Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime? ActivationCodeExpirationTime { get; set; }
        public DateTime? ConfirmedOn { get; set; }

        public virtual void SetActivationCode(Guid activationCode, DateTime expiration)
        {
            if (activationCode == Guid.Empty)
                throw new ArgumentException("The activation code is not definied.", "activationCode");

            if (expiration <= DateTime.Now)
                throw new ArgumentException("The expiration date cannot be less then the actual date/time.", "expiration");

            ActivationCode = activationCode;
            ActivationCodeExpirationTime = expiration;
        }

        public void ConfirmEmail(DateTime confirmationTime)
        {
            if (Provider == "OAuth")
            {
                IsEmailConfirmed = true;
                ConfirmedOn = confirmationTime;
                ActivationCode = new Guid();
                ActivationCodeExpirationTime = null;   
            }
        }
    }
}