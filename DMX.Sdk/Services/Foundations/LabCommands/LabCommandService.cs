// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabCommandService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand)
        {
            var maybeLabCommand =
                await this.dmxApiBroker.PostLabCommandAsync(labCommand);

            return maybeLabCommand;
        }
    }
}
