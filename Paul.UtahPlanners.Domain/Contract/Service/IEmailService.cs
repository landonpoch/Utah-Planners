using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Contract.Service
{
    public interface IEmailService
    {
        bool SendResetEmail(string username, string toEmail, string password);
    }
}
