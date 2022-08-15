// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Tests.Acceptance.Models.Labs;

namespace DMX.Sdk.Tests.Acceptance.Brokers
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand)
        {
            return await this.apiClient.PostContentAsync<LabCommand>(
                relativeUrl: $"{LabCommandsRelativeUrl}",
                content: labCommand);
        }
    }
}
