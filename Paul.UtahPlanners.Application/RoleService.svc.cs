using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

namespace Paul.UtahPlanners.Application
{
    public class RoleService : IRoleService
    {
        public bool IsUserInRole(string username, string roleName)
        {
            return Roles.IsUserInRole(username, roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username);
        }
    }
}
