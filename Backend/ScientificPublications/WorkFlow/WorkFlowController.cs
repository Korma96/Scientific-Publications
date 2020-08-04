using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common.Settings;

namespace ScientificPublications.WorkFlow
{
    public class WorkFlowController : AbstractController
    {
        public WorkFlowController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }


    }
}
