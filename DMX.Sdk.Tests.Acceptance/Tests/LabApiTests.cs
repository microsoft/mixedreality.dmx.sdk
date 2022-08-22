// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Clients;
using DMX.Sdk.Models.Services.Foundations.Labs;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    public partial class LabApiTests : IDisposable
    {
        private readonly DmxClient dmxClient;
        private readonly WireMockServer wireMockServer;

        public LabApiTests()
        {
            this.dmxClient = new DmxClient("http://localhost:1248", "");
            this.wireMockServer = WireMockServer.Start(1248);
        }

        public void Dispose() => this.wireMockServer.Stop();

        private static List<Lab> CreateRandomLabs() =>
            CreateLabsFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Lab> CreateLabsFiller() =>
            new Filler<Lab>();
    }
}
