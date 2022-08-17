// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using Xunit;
using Xeptions;
using DMX.Sdk.Models.LabCommands;
using FluentAssertions;
using Force.DeepCloner;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveLabCommandByIdAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            Guid inputLabCommandId = randomLabCommand.Id;
            LabCommand returnedLabCommand = randomLabCommand;
            LabCommand expectedLabCommand = returnedLabCommand.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(inputLabCommandId))
                    .ReturnsAsync(returnedLabCommand);

            // when
            LabCommand actualLabCommand =
                await this.labCommandService.RetrieveLabCommandByIdAsync(
                    inputLabCommandId);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(inputLabCommandId),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
