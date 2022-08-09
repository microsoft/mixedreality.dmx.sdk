// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Services.Foundations.Labs;
using Microsoft.Extensions.Logging;

namespace DMX.Sdk.Clients
{
    public class DmxClient
    {
        private readonly ILabService labService;

        public DmxClient(
            string environment,
            string secret)
        {
            var dmxApiBroker = new DmxApiBroker(environment, secret);

            ILoggerFactory loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<LoggingBroker>();
            var loggingBroker = new LoggingBroker(logger);

            this.labService = new LabService(dmxApiBroker, loggingBroker);
        }

        public async ValueTask<List<Lab>> RetrieveAllLabsAsync() =>
            await labService.RetrieveAllLabsAsync();


    }
}
