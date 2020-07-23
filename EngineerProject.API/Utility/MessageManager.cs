using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace EngineerProject.API.Utility
{
    public class MessageManager
    {
        private readonly AppSettings appSettings;

        public MessageManager(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public void SendPasswordRecoveryMessage(string email, string code, DateTime expirationDate)
        {
            var smtpClient = ConfigureSmtpClient();

            var template = "test";

            var mailMessage = ConfigureMailMessage("Title", template);

            mailMessage.To.Add(new MailAddress(email));

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
            }
        }

        private MailMessage ConfigureMailMessage(string title, string template) => new MailMessage
        {
            From = new MailAddress(appSettings.SmtpUserName),
            Subject = title,
            IsBodyHtml = true,
            Body = template,
        };

        private SmtpClient ConfigureSmtpClient() => new SmtpClient
        {
            Port = 587,
            Host = appSettings.SmtpServerAddress,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(appSettings.SmtpUserName, appSettings.SmtpUserPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }
}