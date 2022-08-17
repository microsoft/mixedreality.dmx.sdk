// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using Xunit;
using Xeptions;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using FluentAssertions;
using RESTFulSense.Exceptions;
namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabCommands
{
public partial class LabCommandServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveIfCriticalErrorOccursLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            Guid labCommandId = Guid.NewGuid();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(criticalDependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<LabCommand> returnedLabCommand =
                this.labCommandService.RetrieveLabCommandByIdAsync(labCommandId);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    returnedLabCommand.AsTask);

            // then
            actualLabCommandDependencyException
                .Should().BeEquivalentTo(expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(labCommandId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfErrorOccursAndLogItAsync()
        {
            // given
            Guid labCommandId = Guid.NewGuid();
            string someMessage = GetRandomString();

            var someResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    someResponseMessage,
                    someMessage);

            var failedLabDependencyException =
                new FailedLabCommandDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabCommandDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(labCommandId))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabCommand> retrieveByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(labCommandId);

            LabCommandDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    retrieveByIdTask.AsTask);

            // then
            actualLabDependencyException.Should().BeEquivalentTo(
                expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
