// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Net;
using DMX.Sdk.Tests.Acceptance.Models.Labs;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
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

        [Fact]
        public async Task ShouldRetrieveLabCommandByIdAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            Guid randomLabCommandId = randomLabCommand.Id;
            LabCommand expectedLabCommand = randomLabCommand.DeepClone();


            string randomLabCommandCollectionBody =
                JsonConvert.SerializeObject(randomLabCommand);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath($"/api/labcommands/{randomLabCommandId}")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabCommandCollectionBody));

            // when
            LabCommand actualLabCommand =
                await this.dmxApiBroker.GetLabCommandByIdAsync(randomLabCommandId);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);
        }
    }
}
