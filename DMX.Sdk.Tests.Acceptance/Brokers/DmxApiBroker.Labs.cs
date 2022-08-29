﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Tests.Acceptance.Models.Labs;

namespace DMX.Sdk.Tests.Acceptance.Brokers
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            return await this.apiClient.GetContentAsync<List<Lab>>(
                relativeUrl: $"{LabsRelativeUrl}");
        }
    }
}