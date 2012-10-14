using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.MVC3.Helper;
using UtahPlanners.MVC3.PropertyService;

namespace UtahPlanners.MVC3.Services
{
    public interface IServiceFactory
    {
        IPropertyServiceProxy CreatePropertyServiceProxy();
        WcfClient<IPropertyServiceProxy> CreatePropertyServiceWrapper();
        WcfClient<IUserServiceProxy> CreateUserServiceWrapper();
        IFormsAuthenticationService CreateFormsAuthenticationService();
    }
}
