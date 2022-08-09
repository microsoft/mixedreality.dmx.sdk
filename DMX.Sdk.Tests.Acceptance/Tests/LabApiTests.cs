// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Tests.Acceptance.Brokers;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabApiTests
    {
        private readonly DmxApiBroker dmxApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabApiTests(DmxApiBroker dmxApiBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.wireMockServer = WireMockServer.Start(1248);
        }

        private static List<Lab> CreateRandomLabs() =>
            CreateLabsFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Lab> CreateLabsFiller() =>
            new Filler<Lab>();
    }
}
