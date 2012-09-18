using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paul.UtahPlanners.Application.DTO
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}