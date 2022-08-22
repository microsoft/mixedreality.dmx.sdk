// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Services.Foundations.LabCommands;
using Microsoft.Extensions.Logging;

namespace DMX.Sdk.Clients
{
    public partial class LabCommandClient
    {
        private readonly ILabCommandService labCommandService;

        public LabCommandClient(
            string environment,
            string secret)
        {
            var dmxApiBroker = new DmxApiBroker(environment, secret);

            ILoggerFactory loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<LoggingBroker>();
            var loggingBroker = new LoggingBroker(logger);

            this.labCommandService = new LabCommandService(dmxApiBroker, loggingBroker);
        }

        public async ValueTask<LabCommand> SendLabCommandAsync(LabCommand labCommand) =>
            await labCommandService.AddLabCommandAsync(labCommand);

        public async ValueTask<LabCommand> GetLabCommandByIdAsync(Guid labCommandId) =>
            await labCommandService.RetrieveLabCommandByIdAsync(labCommandId);
    }
}
