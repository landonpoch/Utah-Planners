using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Service;
using System.Net.Mail;

namespace UtahPlanners.Infrastructure.Service
{
    public class EmailService : IEmailService
    {

        #region IEmailService Members

        public bool SendEmail(string toEmail, string body)
        {
            try
            {
                var client = new SmtpClient();
                MailMessage message = new MailMessage
                {
                    Body = body
                };
                message.To.Add(new MailAddress(toEmail));
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                // TODO: Logging
                return false;
            }
        }

        #endregion
    }
}
