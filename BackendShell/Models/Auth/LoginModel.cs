using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth
{
    public class LoginModel
    {


        [Required]
        public string Password { get; set; }


        [Required]

        public string UserName { get; set; }
    }
}
