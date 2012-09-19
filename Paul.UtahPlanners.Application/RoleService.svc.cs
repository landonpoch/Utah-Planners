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
        private IRoleService _roleService;

        public RoleService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return _roleService.IsUserInRole(username, roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            return _roleService.GetRolesForUser(username);
        }
    }
}
