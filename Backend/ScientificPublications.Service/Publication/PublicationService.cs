using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.DataAccess.Publication;
using ScientificPublications.Service.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public class PublicationService : AbstractService, IPublicationService
    {
        private readonly IEmailService _emailService;

        private readonly IPublicationDataAccess _publicationDataAccess;

        public PublicationService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            IEmailService emailService,
            IPublicationDataAccess publicationDataAccess) 
            : base(appSettings, mapper)
        {
            _emailService = emailService;
            _publicationDataAccess = publicationDataAccess;
        }
        
        public async Task AcceptPublicationAsync(string email, string authorName, string publicationTitle)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationAcceptedMail);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            var body = string.Format(emailData.Body, authorName, publicationTitle);
            await _emailService.SendEmailAsync(emailData.Subject, body, new string[] { email });
        }

        public async Task DenyPublicationAsync(string email, string authorName, string publicationTitle, string text)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationDeniedMail);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            var body = string.Format(emailData.Body, authorName, publicationTitle, text);
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

        public Task InsertAsync(string fileContent)
        {
            var publication = XmlUtility.Deserialize<DataAccess.Model.publication>(fileContent);
            return _publicationDataAccess.InsertAsync(publication);
        }
        // TO DO: return list of publications 
        public Task<Publications> FindByAuthorAsync(string author)
        {
            return _publicationDataAccess.FindByAuthor(author);
        }

        public Task<Publications> FindByStatusAsync(string status)
        {
            if (!new List<string>(Enum.GetNames(typeof(PublicationStatus))).Contains(status.ToUpper()))
            {
                throw new ValidationException("Status does not exist");
            }

            return _publicationDataAccess.FindByStatusAsync(status.ToLower());
        }
    }
}
