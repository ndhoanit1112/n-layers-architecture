using MailKit.Net.Smtp;
using MimeKit;
using NC.Business.IServices;
using NC.Business.Servives.Base;
using NC.Common;
using NC.Common.Extensions;
using System.Threading.Tasks;

namespace NC.Business.Servives
{
    public class EmailService : BaseService, IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService()
        {
            _smtpSettings = GlobalSettings.GetSmtpSettings();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, string[] bcc = null)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Sender));

            if (bcc != null)
            {
                mimeMessage.Bcc.AddRange(bcc.MapToArray(email => MailboxAddress.Parse(email)));
            }

            mimeMessage.To.Add(MailboxAddress.Parse(toEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using var client = new SmtpClient();
            // Accept all SSL certificates (in case the server supports STARTTLS)
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(_smtpSettings.MailServer, _smtpSettings.MailPort, false);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
