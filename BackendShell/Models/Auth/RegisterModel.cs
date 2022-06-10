using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPasswordConfirmation { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }


    }
}
