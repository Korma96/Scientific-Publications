using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;

namespace ScientificPublications.DataAccess.Review
{
    public class ReviewDataAccess : AbstractDataAccess, IReviewDataAccess
    {
        public ReviewDataAccess(
            IOptions<AppSettings> appSettings, 
            ILogger<AbstractDataAccess> logger) 
            : base(appSettings, logger, string.Empty)
        {
        }

        public async Task AddCommentAsync(string publicationId, string sectionId, string comment)
        {
            try
            {
                var path = BaseUrl.UrlCombine(Constants.JavaController.Publication.UrlCombine($"add-comment/{publicationId}/{sectionId}"));
                using (var client = new HttpClient())
                {
                    var content = new StringContent(comment, Encoding.UTF8, Constants.XmlContentType);
                    var result = await client.PutAsync(path, content);
                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
