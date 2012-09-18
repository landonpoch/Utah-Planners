using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.Contract.Service
{
    public interface IEmailService
    {
        bool SendEmail(string toEmail, string body);
    }
}
