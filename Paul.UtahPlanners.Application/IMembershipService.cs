using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UtahPlanners.Domain.Entity;

namespace Paul.UtahPlanners.Application
{
    [ServiceContract]
    public interface IMembershipService
    {
        [OperationContract]
        int GetMinPasswordLength();

        [OperationContract]
        bool ValidateUser(string userName, string password);

        [OperationContract]
        MembershipStatus CreateUser(string userName, string password, string email);

        [OperationContract]
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
