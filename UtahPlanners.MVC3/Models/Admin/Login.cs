using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Models.Admin
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateAccount
    {
        // TODO: Required fields
        public string Username { get; set; }
        public string Password { get; set; }
        // TODO: Client-side regex validators
        public string Email { get; set; }
    }
}