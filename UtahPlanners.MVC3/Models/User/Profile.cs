using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UtahPlanners.MVC3.Models.User
{
    public class Profile
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "User Name")]
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string Role { get; set; }
        
        public string Theme { get; set; }
        
        public SelectList Themes { get; set; }
        
        public ChangePassword ChangePassword { get; set; }
    }
}