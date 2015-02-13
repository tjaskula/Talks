using System;

namespace Domain
{
    public class Account : IEquatable<Account>
    {
        public Account(string email, string password, 
                        string provider, bool isEmailConfirmed, Guid activationCode,
                        DateTime? activationCodeExpirationTime, DateTime? confirmedOn)
        {
            Email = email;
            Password = password;
            Provider = provider;
            IsEmailConfirmed = isEmailConfirmed;
            ActivationCode = activationCode;
            ActivationCodeExpirationTime = activationCodeExpirationTime;
            ConfirmedOn = confirmedOn;
        }

        public Account(string email, string password, string provider)
        {
            Email = email;
            Password = password;
            Provider = provider;
        }

        private Account()
        {
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Provider { get; private set; }
        public bool IsEmailConfirmed { get; private set; }
        public Guid ActivationCode { get; private set; }
        public DateTime? ActivationCodeExpirationTime { get; private set; }
        public DateTime? ConfirmedOn { get; private set; }

        public static Account Empty
        {
            get
            {
                return new Account { Email = "Empty" };
            }
        }

        public void SetActivationCode(Guid activationCode, DateTime expiration)
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
            IsEmailConfirmed = true;
            ConfirmedOn = confirmationTime;
            ActivationCode = new Guid();
            ActivationCodeExpirationTime = null;
        }

        public bool Equals(Account other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(Email, other.Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Account)obj);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }

        public static bool operator ==(Account left, Account right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Account left, Account right)
        {
            return !Equals(left, right);
        }
    }
}