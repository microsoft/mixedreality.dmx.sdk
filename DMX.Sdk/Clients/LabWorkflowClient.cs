// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Services.Foundations.LabWorkflows;
using Microsoft.Extensions.Logging;

namespace DMX.Sdk.Clients
{
    public class LabWorkflowClient
    {
        private readonly ILabWorkflowService labWorkflowService;

        public LabWorkflowClient(
            string environment,
            string secret)
        {
            var dmxApiBroker = new DmxApiBroker(environment, secret);

            ILoggerFactory loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<LoggingBroker>();
            var loggingBroker = new LoggingBroker(logger);

            this.labWorkflowService = new LabWorkflowService(dmxApiBroker, loggingBroker);
        }

        public async ValueTask<LabWorkflow> SendLabWorkflowAsync(LabWorkflow labWorkflow) =>
            await labWorkflowService.AddLabWorkflowAsync(labWorkflow);
    }
}
