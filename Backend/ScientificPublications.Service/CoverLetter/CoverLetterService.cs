using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.CoverLetter;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.CoverLetter
{
    public class CoverLetterService : AbstractService, ICoverLetterService
    {
        private readonly ICoverLetterDataAccess _coverLetterDataAccess;

        public CoverLetterService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            ICoverLetterDataAccess coverLetterDataAccess) : base(appSettings, mapper)
        {
            _coverLetterDataAccess = coverLetterDataAccess;
        }

        public async Task<MemoryStream> GetXsdSchemaAsync()
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.CoverLetterXsd);
            return await FileUtility.ReadAsStreamAsync(path);
        }

        public Task InsertAsync(string fileContent)
        {
            var coverLetter = XmlUtility.Deserialize<DataAccess.Model.CoverLetter>(fileContent);
            return _coverLetterDataAccess.InsertAsync(coverLetter);
        }

        public Task<DataAccess.Model.CoverLetter> FindByPublicationIdAsync(string publicationId)
        {
            return _coverLetterDataAccess.FindByPublicationIdAsync(publicationId);
        }

        public void ValidateCoverLetterFile(string fileContent)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.CoverLetterXsd);
            var xDocument = XmlUtility.Parse(fileContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }
    }
}
