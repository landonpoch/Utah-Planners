using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ninject;
using System.Web.Mvc;
using UtahPlanners.MVC3.Extensions;

namespace UtahPlanners.MVC3.Services
{
    public class WcfRoleProvider : RoleProvider
    {
        public IServiceFactory _factory;

        public WcfRoleProvider()
        {
            // Using service locator anti-pattern due to asp.net framework limitation.
            // See: http://stackoverflow.com/questions/6519720/using-ninject-with-a-custom-role-provider-in-an-mvc3-app
            _factory = DependencyResolver.Current.GetService<IServiceFactory>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var wcf = _factory.CreateUserService())
            {
                return wcf.Client.IsUserInRole(username, roleName);
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var wcf = _factory.CreateUserService())
            {
                return wcf.Client.GetRolesForUser(username);
            }
        }

        #region Unimplemented Methods
        
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}