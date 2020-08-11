using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.DataAccess.Publication;
using ScientificPublications.Service.Email;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<PublicationStatus> GetStatusAsync(string id)
        {
            var publication = await GetByIdWithValidationAsync(id);
            return HelperMethods.StatusStringToEnum(publication.header.status);
        }

        public async Task<publication> GetByIdWithValidationAsync(string id)
        {
            var publication = await GetByIdAsync(id);
            if (publication == null)
            {
                throw new ValidationException(Constants.ExceptionMessages.DoesNotExist);
            }
            return publication;
        }

        public Task<publication> GetByIdAsync(string id)
        {
            return _publicationDataAccess.FindByIdAsync(id);
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
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsd);
            var xDocument = XmlUtility.Parse(fileContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }

        public async Task<MemoryStream> GetXsdSchemaAsync()
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsd);
            return await FileUtility.ReadAsStreamAsync(path);
        }

        public Task InsertAsync(string fileContent)
        {
            var publication = XmlUtility.Deserialize<publication>(fileContent);
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

        public Task UpdateStatusAsync(string publicationId, PublicationStatus status)
        {
            return _publicationDataAccess.UpdateStatusAsync(publicationId, status);
        }

        public async Task UpdateStatusWithValidationAsync(string publicationId, string nextStatus, string userRole)
        {
            var currentStatus = await GetStatusAsync(publicationId);
            HelperMethods.CheckIsNextStateValid(currentStatus, nextStatus, userRole);
            await UpdateStatusAsync(publicationId, HelperMethods.StatusStringToEnum(nextStatus));
        }

        public Task<Publications> FindBySearchQueryAsync(string searchQuery)
        {
            return _publicationDataAccess.FindBySearchQueryAsync(searchQuery);
        }

        public Task<Publications> FindMyBySearchQueryAsync(string username, string searchQuery)
        {
            return _publicationDataAccess.FindByUsernameAndSearchQueryAsync(username, searchQuery);
        }
    }
}
