using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtahPlanners.MVC3.Services
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}