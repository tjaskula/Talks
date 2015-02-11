using System.ComponentModel.DataAnnotations;
using Api.Attributs;

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
        [BooleanRequired]
        public bool Privacy { get; set; }

        [Required]
        public string Provider { get; set; }
    }
}