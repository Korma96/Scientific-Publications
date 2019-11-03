using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.Service.Email;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public class PublicationService : AbstractService, IPublicationService
    {
        private readonly IEmailService _emailService;

        public PublicationService(IOptions<AppSettings> options, IEmailService emailService) : base(options)
        {
            _emailService = emailService;
        }

        public async Task AcceptPublicationAsync(string email)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationAcceptedMail);
            var emailData = XmlUtility.Deserialize<EmailEntity>(path);
            var body = string.Format(emailData.Body, "Mr. Author", "Applied AI");
            await _emailService.SendEmailAsync(emailData.Subject, body, new string[] { email });
        }

        public async Task DenyPublicationAsync(string email, string text)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationDeniedMail);
            var emailData = XmlUtility.Deserialize<EmailEntity>(path);
            var body = string.Format(emailData.Body, "Mr. Author", "Applied AI", text);
            await _emailService.SendEmailAsync(emailData.Subject, body, new string[] { email });
        }
        
        public void ValidatePublicationFile(string fileContent)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsdSchema);
            var xDocument = XmlUtility.Parse(fileContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }

        public async Task<MemoryStream> GetXsdSchemaAsync()
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsdSchema);
            return await FileUtility.ReadAsStreamAsync(path);
        }
    }
}
