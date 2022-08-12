// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Tests.Acceptance.Brokers;
using DMX.Sdk.Tests.Acceptance.Models.Labs;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabCommandApiTests
    {
        private readonly DmxApiBroker dmxApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabCommandApiTests(DmxApiBroker dmxApiBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.wireMockServer = WireMockServer.Start(1248);
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
    }
}
