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
using ScientificPublications.DataAccess.Model;

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

        public async Task AddEvaluationAsync(string publicationId, evaluation evaluation)
        {
            try
            {
                var path = BaseUrl.UrlCombine(Constants.JavaController.Evaluation.UrlCombine($"insert/{evaluation.author}/{evaluation.publicationId}"));
                await HttpHelper.Post(path, evaluation);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Evaluations> GetEvaluationAsync(string publicationId, string reviewerUsername)
        {
            try
            {
                var path = BaseUrl.UrlCombine(Constants.JavaController.Evaluation.UrlCombine($"by-publicationId-reviewer/{publicationId}/{reviewerUsername}"));
                return await HttpHelper.Get<Evaluations>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Evaluations> GetEvaluationsAsync(string publicationId)
        {
            try
            {
                var path = BaseUrl.UrlCombine(Constants.JavaController.Evaluation.UrlCombine($"by-publicationId/{publicationId}"));
                return await HttpHelper.Get<Evaluations>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
