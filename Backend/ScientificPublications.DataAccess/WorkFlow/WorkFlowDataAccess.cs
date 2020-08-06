﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using System;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.WorkFlow
{
    public class WorkFlowDataAccess : AbstractDataAccess, IWorkFlowDataAccess
    {
        public WorkFlowDataAccess(
            IOptions<AppSettings> appSettings, 
            ILogger<AbstractDataAccess> logger) 
            : base(appSettings, logger, Constants.JavaController.WorkFlow)
        {
        }

        public async Task InsertAsync(Model.workflow workFlow)
        {
            try
            {
                await HttpHelper.Post(BaseUrl, workFlow);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
