// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Artifacts;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string ArtifactsRelativeUrl = "api/artifacts";

        public async ValueTask<Artifact> PostArtifactsAsync(Artifact lab)
        {
            await GetAccessTokenForScope("PostLab");

            return await PostAsync<Artifact>(ArtifactsRelativeUrl, lab);
        }
    }
}
