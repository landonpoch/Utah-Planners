using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.DTO;

namespace Paul.UtahPlanners.Application.Service
{
    public class DefaultMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider = Membership.Provider;

        #region IMembershipService Members

        public int GetMinPasswordLength()
        {
            return _provider.MinRequiredPasswordLength;
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public global::UtahPlanners.Domain.Entity.MembershipStatus CreateUser(DTO.CreateUserRequest request)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(request.Username,
                request.Password,
                request.Email,
                request.SecurityQuestion,
                request.SecurityAnswer,
                true,
                null,
                out status);

            return (MembershipStatus)(int)status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return _provider.ChangePassword(userName, oldPassword, newPassword);
        }

        public User GetUser(string username)
        {
            var user = _provider.GetUser(username, false);
            return Convert(user);
        }

        public string ResetPassword(string username, string answer)
        {
            return _provider.ResetPassword(username, answer);
        }

        public bool ChangeEmail(string username, string email)
        {
            bool result = false;
            try
            {
                var user = _provider.GetUser(username, true);
                user.Email = email;
                _provider.UpdateUser(user);
                result = true;
            }
            catch (Exception e)
            {
                // TODO: Logging
            }
            return result;
        }

        #endregion

        private User Convert(MembershipUser user)
        {
            return new User
            {
                Username = user.UserName,
                Email = user.Email,
                SecurityQuestion = user.PasswordQuestion
            };
        }
    }
}