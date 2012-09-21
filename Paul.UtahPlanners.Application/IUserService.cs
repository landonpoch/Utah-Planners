using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.DTO;

namespace Paul.UtahPlanners.Application
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        int GetMinPasswordLength();

        [OperationContract]
        bool ValidateUser(string userName, string password);

        [OperationContract]
        MembershipStatus CreateUser(CreateUserRequest request);

        [OperationContract]
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        bool ResetPassword(string username, string email, string answer);

        [OperationContract]
        bool IsUserInRole(string username, string roleName);

        [OperationContract]
        string[] GetRolesForUser(string username);
    }
}