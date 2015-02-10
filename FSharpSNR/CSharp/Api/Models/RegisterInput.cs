using System.ComponentModel.DataAnnotations;
using Api.Attributs;

namespace Api.Models
{
    public class RegisterInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Pass { get; set; }

        [Required]
        [BooleanRequired]
        public bool Privacy { get; set; }
    }
}