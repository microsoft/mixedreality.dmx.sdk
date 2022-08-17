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

        public ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand) =>
        TryCatch(async () =>
        {
            ValidateLabCommand(labCommand);

            return await this.dmxApiBroker.PostLabCommandAsync(labCommand);
        });

        public ValueTask<LabCommand> RetrieveLabCommandByIdAsync(Guid labCommandId)
        {
            throw new NotImplementedException();
        }
    }
}
