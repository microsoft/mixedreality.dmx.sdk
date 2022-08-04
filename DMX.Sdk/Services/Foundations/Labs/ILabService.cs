// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Services.Foundations.Labs
{
    public interface ILabService
    {
        ValueTask<List<Lab>> RetrieveAllLabsAsync();
    }
}
