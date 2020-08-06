using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.DataAccess.WorkFlow;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.WorkFlow
{
    public class WorkFlowService : AbstractService, IWorkFlowService
    {
        private readonly IWorkFlowDataAccess _workFlowDataAccess;

        public WorkFlowService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            IWorkFlowDataAccess workFlowDataAccess) 
            : base(appSettings, mapper)
        {
            _workFlowDataAccess = workFlowDataAccess;
        }

        public Task InsertAsync(workflow workFlow)
        {
            return _workFlowDataAccess.InsertAsync(workFlow);
        }

        public void Validate(workflow workflow)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.WorkFlowXsd);
            var xDocument = XmlUtility.Parse(XmlUtility.Serialize(workflow));
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }
    }
}
