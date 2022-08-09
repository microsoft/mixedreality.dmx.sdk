// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using RESTFulSense.Clients;

namespace DMX.Sdk.Tests.Acceptance.Brokers
{
    public partial class DmxApiBroker
    {
        public readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public DmxApiBroker()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://localhost:1248");
            this.apiClient = new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
