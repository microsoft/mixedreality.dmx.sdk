// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Tests.Acceptance.Brokers;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Sdk.Tests.Acceptance.Clients
{
    public partial class LabApiTests
    {
        [Fact]
        public async Task ShouldGetAllLabsAsync()
        {
            // given
            List<Lab> randomLabs = CreateRandomLabs();
            List<Lab> expectedLabs = randomLabs;

            string randomLabsCollectionBody =
                JsonConvert.SerializeObject(randomLabs);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labs")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabsCollectionBody));

            // when
            List<Lab> actualLabs =
                await this.dmxApiBroker.GetAllLabsAsync();

            // then
            actualLabs.Should().BeEquivalentTo(expectedLabs);
        }

        public void Dispose()
        {
            this.wireMockServer.Stop();
        }
    }
}
