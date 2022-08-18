// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Clients
{
    public partial class DmxClient
    {
        public async ValueTask<LabCommand> SendLabCommandAsync(LabCommand labCommand) =>
            await labCommandService.AddLabCommandAsync(labCommand);

        public async ValueTask<LabCommand> GetLabCommandByIdAsync(Guid labCommandId) =>
            await labCommandService.RetrieveLabCommandByIdAsync(labCommandId);
    }
}
