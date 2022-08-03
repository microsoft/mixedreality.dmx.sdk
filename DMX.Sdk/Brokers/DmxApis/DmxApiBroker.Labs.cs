// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            await GetAccessTokenForScope("GetAllLabs");

            return await GetAsync<List<Lab>>(LabsRelativeUrl);
        }

        public async ValueTask<Lab> PostLabAsync(Lab lab)
        {
            await GetAccessTokenForScope("PostLab");

            return await PostAsync<Lab>(LabsRelativeUrl, lab);
        }
    }
}
