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
        private const string ResetPasswordEmailSubject = "Utahplanners.com - Password Reset";
        private const string ResetPasswordEmailBody = "{0},\r\n\r\nYour Utahplanners.com password has been reset.  Your new temporary password is:\r\n\r\n{1}";

        #region IEmailService Members

        public bool SendResetEmail(string username, string toEmail, string password)
        {
            try
            {
                var client = new SmtpClient();
                MailMessage message = new MailMessage
                {
                    Subject = ResetPasswordEmailSubject,
                    Body = String.Format(ResetPasswordEmailBody, username, password),
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
