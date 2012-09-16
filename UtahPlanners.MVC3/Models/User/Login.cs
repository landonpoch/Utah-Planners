using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UtahPlanners.MVC3.Models.User
{
    public class Login
    {
        // TODO: Fix these display properties. Colon should be in the markup.
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}