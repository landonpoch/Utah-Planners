using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.MVC3.Presentation;

namespace UtahPlanners.MVC3.Services
{
    public interface IServiceFactory
    {
        WcfClient<IPropertyServiceProxy> CreatePropertyService();
        WcfClient<IUserServiceProxy> CreateUserService();
        IFormsAuthenticationService CreateFormsAuthenticationService();
    }
}
