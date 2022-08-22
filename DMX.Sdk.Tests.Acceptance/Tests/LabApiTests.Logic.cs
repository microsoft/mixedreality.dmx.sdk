// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Net;
using DMX.Sdk.Models.Services.Foundations.Labs;
using FluentAssertions;
using Newtonsoft.Json;
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
                await this.dmxClient.Labs.GetAllLabsAsync();

            // then
            actualLabs.Should().BeEquivalentTo(expectedLabs);
        }
    }
}
