// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<List<Lab>> GetAllLabsAsync();
        ValueTask<Lab> PostLabAsync(Lab lab);
    }
}
