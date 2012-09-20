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
    public interface IMembershipService
    {
        int GetMinPasswordLength();
        bool ValidateUser(string userName, string password);
        MembershipStatus CreateUser(CreateUserRequest request);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        User GetUser(string username);
        string ResetPassword(string username, string answer);
    }
}
