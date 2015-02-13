using System;

namespace Infrastructure.EF
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime? ActivationCodeExpirationTime { get; set; }
        public DateTime? ConfirmedOn { get; set; }
    }
}