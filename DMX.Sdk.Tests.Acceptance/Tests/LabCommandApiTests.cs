// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Clients;
using DMX.Sdk.Models.LabCommands;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    public partial class LabCommandApiTests : IDisposable
    {
        private readonly DmxClient dmxClient;
        private readonly WireMockServer wireMockServer;

        public LabCommandApiTests()
        {
            this.dmxClient = new DmxClient("http://localhost:1249", "");
            this.wireMockServer = WireMockServer.Start(1249);
        }

        private static LabCommand CreateRandomLabCommand() =>
            CreateLabCommandFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabCommand> CreateLabCommandFiller()
        {
            var filler = new Filler<LabCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
