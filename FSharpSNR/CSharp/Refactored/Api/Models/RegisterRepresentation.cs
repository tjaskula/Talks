using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class RegisterRepresentation
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool Privacy { get; set; }

        [Required]
        public string Provider { get; set; }

        public ConfirmationRepresentation Confirmation { get; set; }
    }
}