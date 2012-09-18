using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UtahPlanners.MVC3.Models.User
{
    public class ForgotPassword
    {
        [Display(Name = "User Name")]
        [Required]
        public string Username { get; set; }

        public string SecurityQuestion { get; set; }
        
        public string SecurityAnswer { get; set; }
    }
}