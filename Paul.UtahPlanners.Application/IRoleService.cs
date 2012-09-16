using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Paul.UtahPlanners.Application
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRoleService" in both code and config file together.
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        bool IsUserInRole(string username, string roleName);

        [OperationContract]
        string[] GetRolesForUser(string username);
    }
}
