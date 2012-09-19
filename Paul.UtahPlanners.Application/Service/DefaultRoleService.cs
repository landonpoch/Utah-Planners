using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Paul.UtahPlanners.Application.Service
{
    public class DefaultRoleService : IRoleService
    {
        #region IRoleService Members

        public bool IsUserInRole(string username, string roleName)
        {
            return Roles.IsUserInRole(username, roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username);
        }

        #endregion
    }
}