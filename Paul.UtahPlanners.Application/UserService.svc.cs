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
using Paul.UtahPlanners.Application.Contract;

namespace Paul.UtahPlanners.Application
{
    public class UserService : IUserService
    {
        private IMembershipService _membershipService;
        private IProfileService _profileService;
        private IRoleService _roleService;
        private IEmailService _emailService;

        public UserService(IMembershipService membershipService,
            IProfileService profileService,
            IRoleService roleService,
            IEmailService emailService)
        {
            _membershipService = membershipService;
            _profileService = profileService;
            _roleService = roleService;
            _emailService = emailService;
        }

        #region IUserService Members

        public int GetMinPasswordLength()
        {
            return _membershipService.GetMinPasswordLength();
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) 
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) 
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return _membershipService.ValidateUser(userName, password);
        }

        public MembershipStatus CreateUser(CreateUserRequest request)
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

            return _membershipService.CreateUser(request);
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
                User currentUser = _membershipService.GetUser(userName);
                return _membershipService.ChangePassword(userName, oldPassword, newPassword);
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
            var user = _membershipService.GetUser(username);
            var profile = _profileService.GetUserProfile(user.Username);
            var role = _roleService.GetRolesForUser(user.Username);
            user.UserProfile = profile;
            user.Role = role.FirstOrDefault();
            return user;
        }

        public bool UpdateUser(User user)
        {
            var result = _profileService.UpdateUserProfile(user.Username, user.UserProfile);
            return result && _membershipService.ChangeEmail(user.Username, user.Email);
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
                string newPassword = _membershipService.ResetPassword(username, answer);
                result = _emailService.SendResetEmail(username, email, newPassword);
            }
            catch (MembershipPasswordException e)
            {
                result = false;
            }
            return result;
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return _roleService.IsUserInRole(username, roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            return _roleService.GetRolesForUser(username);
        }

        #endregion
    }
}
