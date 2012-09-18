using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.DTO;

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
        MembershipStatus CreateUser(CreateUserRequest request);

        [OperationContract]
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
