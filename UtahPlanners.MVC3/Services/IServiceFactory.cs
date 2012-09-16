using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.MVC3.RoleService;

namespace UtahPlanners.MVC3.Services
{
    public interface IServiceFactory
    {
        IPropertyServiceProxy CreatePropertyService();
        IMembershipServiceProxy CreateMembershipService();
        IRoleServiceProxy CreateRoleService();
        IFormsAuthenticationService CreateFormsAuthenticationService();
    }
}
