using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paul.UtahPlanners.Application.DTO;
using System.Web.Profile;
using Paul.UtahPlanners.Application.Contract;

namespace Paul.UtahPlanners.Application.Service
{
    public class DefaultProfileService : IProfileService
    {
        public UserProfile GetUserProfile(string username)
        {
            // TODO: Harden this method
            ProfileBase profile = ProfileBase.Create(username, true);
            return new UserProfile
            {
                FirstName = profile["fname"].ToString(),
                LastName = profile["lname"].ToString(),
                Theme = profile["Theme"].ToString()
            };
        }

        public bool UpdateUserProfile(string username, UserProfile userProfile)
        {
            bool result = false;
            try
            {
                ProfileBase profile = ProfileBase.Create(username, true);
                profile["fname"] = userProfile.FirstName;
                profile["lname"] = userProfile.LastName;
                profile["Theme"] = userProfile.Theme;
                profile.Save();
                result = true;
            }
            catch (Exception e)
            {
                // TODO: Logging
            }
            return result;
        }
    }
}