// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Services.Foundations.LabCommands;
using DMX.Sdk.Services.Foundations.Labs;
using Microsoft.Extensions.Logging;

namespace DMX.Sdk.Clients
{
    public partial class DmxClient
    {
        public LabCommandClient LabCommands { get; private set; }
        public LabClient Labs { get; private set; }

        public DmxClient(
            string environment,
            string secret)
        {
            this.LabCommands = new LabCommandClient(environment, secret);

            this.Labs = new LabClient(environment, secret);
        }
    }
}
