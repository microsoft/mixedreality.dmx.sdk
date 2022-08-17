// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

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

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(httpResponseException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(labCommandId))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabCommand> retrieveByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(labCommandId);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    retrieveByIdTask.AsTask);

            // then
            actualLabCommandDependencyException.Should().BeEquivalentTo(
                expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Guid labCommandId = Guid.NewGuid();
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            var httpBadRequestException =
                new HttpResponseBadRequestException(
                    httpMessage,
                    randomMessage);

            httpBadRequestException.AddData(randomDictionary);

            var invalidLabCommandException =
                new InvalidLabCommandException(
                    httpBadRequestException,
                    randomDictionary);

            var expectedLabCommandDependencyValidationException =
                new LabCommandDependencyValidationException(invalidLabCommandException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<LabCommand> retrieveByIdLabCommandTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(labCommandId);

            LabCommandDependencyValidationException actualLabCommandDependencyValidationException =
                await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                    retrieveByIdLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyValidationException.Should()
                .BeEquivalentTo(expectedLabCommandDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveIfErrorOccursAndLogItAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedlLabCommandServiceException =
                new FailedLabCommandServiceException(serviceException);

            var expectedLabCommandServiceException =
                new LabCommandServiceException(failedlLabCommandServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabCommand> getByIdLabCommandTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(randomId);

            LabCommandServiceException actualLabCommandServiceException =
                await Assert.ThrowsAsync<LabCommandServiceException>(
                    getByIdLabCommandTask.AsTask);

            // then
            actualLabCommandServiceException.Should()
                .BeEquivalentTo(expectedLabCommandServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
