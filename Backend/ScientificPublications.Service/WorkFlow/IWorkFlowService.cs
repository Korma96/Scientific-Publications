using ScientificPublications.Common.Enums;
using ScientificPublications.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificPublications.Service.WorkFlow
{
    public interface IWorkFlowService : ISingletonService
    {
        void Validate(workflow workflow);

        Task InsertAsync(workflow workFlow);

        Task<workflow> FindByPublicationIdAsync(string publicationId);

        Task AcceptPublicationAsync(string publicationId, bool accepted, string reviewerUsername);

        Task<List<ReviewerPublicationDto>> GetByReviewerAsync(string username);
    }
}
