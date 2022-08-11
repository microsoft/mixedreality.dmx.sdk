// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Services.Foundations.LabCommands
{
    public interface ILabCommandService
    {
        ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand);
    }
}