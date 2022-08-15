// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Clients
{
    public partial class DmxClient
    {
        public async ValueTask<List<Lab>> RetrieveAllLabsAsync() =>
            await labService.RetrieveAllLabsAsync();
    }
}
