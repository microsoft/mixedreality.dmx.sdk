using DMX.Sdk.Models.Services.Foundations.Labs;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllLabsAsync()
        {
            // given
            List<Lab> randomLabs = CreateRandomLabs();
            var returnedLabs = randomLabs;
            var expectedLabs = randomLabs.DeepClone();

            this.dmxApiBroker.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ReturnsAsync(returnedLabs);

            // when
            List<Lab> actualLabs = await this.labService.RetrieveAllLabsAsync();

            // then
            actualLabs.Should().BeEquivalentTo(expectedLabs);

            this.dmxApiBroker.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
