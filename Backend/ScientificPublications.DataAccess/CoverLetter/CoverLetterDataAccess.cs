using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;

namespace ScientificPublications.DataAccess.CoverLetter
{
    public class CoverLetterDataAccess : AbstractDataAccess, ICoverLetterDataAccess
    {
        public CoverLetterDataAccess(
            IOptions<AppSettings> appSettings, 
            ILogger<AbstractDataAccess> logger) 
            : base(appSettings, logger, Constants.JavaController.CoverLetter)
        {
        }
        
        public async Task InsertAsync(Model.CoverLetter coverLetter)
        {
            try
            {
                await HttpHelper.Post(BaseUrl, coverLetter);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
        // TODO: prevent user to access forbidden cover letters
        public async Task<Model.CoverLetter> FindByPublicationIdAsync(string publicationId)
        {
            try
            {
                var path = BaseUrl.UrlCombine(publicationId);
                return await HttpHelper.Get<Model.CoverLetter>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
