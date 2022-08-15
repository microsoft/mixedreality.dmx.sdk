// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand);
    }
}
