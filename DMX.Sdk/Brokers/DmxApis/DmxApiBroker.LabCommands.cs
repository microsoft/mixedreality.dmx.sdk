// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand) =>
            await PostAsync(LabCommandsRelativeUrl, labCommand);
    }
}
