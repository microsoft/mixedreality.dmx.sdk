// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Models.Services.Foundations.Labs;

namespace DMX.Sdk.Services.Foundations.Labs
{
    public partial class LabService : ILabService
    {
        private readonly IDmxApiBroker dmxApiBroker;

        public LabService(IDmxApiBroker dmxApiBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
        }

        public ValueTask<List<Lab>> RetrieveAllLabsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
