﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.MVC3.Services
{
    public interface IServiceFactory
    {
        IPropertyServiceProxy CreatePropertyService();
        IUserServiceProxy CreateUserService();
        IFormsAuthenticationService CreateFormsAuthenticationService();
    }
}
