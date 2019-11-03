using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;

namespace ScientificPublications.Service
{
    public abstract class AbstractService
    {
        protected AppSettings AppSettings { get; private set; }

        public AbstractService(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }
    }
}
