using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UtahPlanners.MVC3.Models.User
{
    public class CreateAccount
    {
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        // TODO: Client-side regex validators
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Security Question")]
        [Required]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Answer")]
        [Required]
        public string SecurityAnswer { get; set; }
    }
}