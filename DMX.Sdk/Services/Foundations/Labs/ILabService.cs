// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Services.Foundations.Labs
{
    public interface ILabService
    {
        ValueTask<Lab> AddLabAsync(Lab lab);
        ValueTask<List<Lab>> RetrieveAllLabsAsync();
    }
}
