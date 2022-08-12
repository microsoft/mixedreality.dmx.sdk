// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Tests.Acceptance.Models.Labs;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    public partial class LabCommandApiTests
    {
        [Fact]
        public async Task ShouldAddLabCommandAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand inputLabCommand = randomLabCommand;
            LabCommand expectedLabCommand = randomLabCommand.DeepClone();

            string randomLabCommandCollectionBody =
                JsonConvert.SerializeObject(randomLabCommand);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labcommands")
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabCommandCollectionBody));

            // when
            LabCommand actualLabCommand =
                await this.dmxApiBroker.PostLabCommandAsync(inputLabCommand);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);
        }

        public void Dispose()
        {
            this.wireMockServer.Stop();
        }
    }
}
