// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            return await GetAsync<List<Lab>>(LabsRelativeUrl);
        }
    }
}
