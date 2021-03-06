﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paul.UtahPlanners.Application.DTO
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string SecurityQuestion { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}