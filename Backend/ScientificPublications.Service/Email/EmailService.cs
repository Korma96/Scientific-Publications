using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task SendEmailAsync(string subject, string body, string[] to)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_appSettings.Smtp.Username),
                Subject = subject,
                Body = body
            };
            
            foreach(var recipient in to)
                message.To.Add(recipient);

            using (var client = GetSmtpClient())
            {
                await client.SendMailAsync(message);
            }
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = _appSettings.Smtp.Host,
                Port = _appSettings.Smtp.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_appSettings.Smtp.Username, _appSettings.Smtp.Password)
            };
        }
    }
}
