using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using UtahPlanners.Domain.Entity;
using Paul.UtahPlanners.Application.DTO;
using System.Net.Mail;
using UtahPlanners.Domain.Contract.Service;
using System.Web;
using System.Web.Profile;

namespace Paul.UtahPlanners.Application
{
    public class MembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider = Membership.Provider;
        private IEmailService _emailService;
        private IRoleService _roleService;

        public MembershipService(IEmailService emailService, IRoleService roleService)
        {
            _emailService = emailService;
            _roleService = roleService;
        }

        #region IMembershipService Members

        public int GetMinPasswordLength()
        {
            return _provider.MinRequiredPasswordLength;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) 
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) 
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public global::UtahPlanners.Domain.Entity.MembershipStatus CreateUser(CreateUserRequest request)
        {
            if (String.IsNullOrEmpty(request.Username)) 
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(request.Password)) 
                throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(request.Email)) 
                throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(request.SecurityQuestion))
                throw new ArgumentException("Value cannot be null or empty.", "securityQuestion");
            if (String.IsNullOrEmpty(request.SecurityAnswer))
                throw new ArgumentException("Value cannot be null or empty.", "securityAnswer");

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
            if (String.IsNullOrEmpty(userName)) 
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) 
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) 
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public User GetUser(string username)
        {
            var user = _provider.GetUser(username, false);
            return Convert(user);
        }

        public bool ResetPassword(string username, string email, string answer)
        {
            if (String.IsNullOrEmpty(username))
                throw new ArgumentException("Value cannot be null or empty.", "username");
            if (String.IsNullOrEmpty(answer))
                throw new ArgumentException("Value cannot be null or empty.", "answer");

            bool result = false;
            try
            {
                string newPassword = _provider.ResetPassword(username, answer);
                result = _emailService.SendResetEmail(username, email, newPassword);
            }
            catch (MembershipPasswordException e)
            {
                result = false;
            }
            return result;
        }

        #endregion

        private User Convert(MembershipUser user)
        {
            ProfileBase profile = ProfileBase.Create(user.UserName, true);
            return new User
            {
                Username = user.UserName,
                FirstName = profile["fname"] != null ? profile["fname"].ToString() : String.Empty,
                LastName = profile["lname"] != null ? profile["lname"].ToString() : String.Empty,
                Email = user.Email,
                Role = _roleService.GetRolesForUser(user.UserName).FirstOrDefault(),
                SecurityQuestion = user.PasswordQuestion
            };
        }
    }
}
