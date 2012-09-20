using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paul.UtahPlanners.Application.DTO;

namespace Paul.UtahPlanners.Application.Contract
{
    public interface IProfileService
    {
        UserProfile GetUserProfile(string username);
        bool UpdateUserProfile(string username, UserProfile userProfile);
    }
}