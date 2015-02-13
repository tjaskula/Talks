namespace Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ConfirmationRepresentation
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid Code { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }
    }
}