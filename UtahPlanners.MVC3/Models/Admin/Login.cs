using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class Login
    {
        // TODO: Fix these display properties. Colon should be in the markup.
        [Display(Name = "Username: ")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Password: ")]
        [Required]
        public string Password { get; set; }
    }

    public class CreateAccount
    {
        [Display(Name = "Username: ")]
        [Required]
        public string Username { get; set; }
        
        [Display(Name = "Password: ")]
        [Required]
        public string Password { get; set; }
        
        // TODO: Client-side regex validators
        [Display(Name = "Email: ")]
        [Required]
        public string Email { get; set; }
    }
}