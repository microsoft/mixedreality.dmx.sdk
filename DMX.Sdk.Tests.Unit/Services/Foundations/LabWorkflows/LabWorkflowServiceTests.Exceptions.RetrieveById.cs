// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionAndLogItAsync(Exception criticalException)
        {
            // given
            Guid someRandomId = Guid.NewGuid();

            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(criticalException);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyException(failedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalException);

            // when
            ValueTask<LabWorkflow> retrieveLabWorkflowByIdTask =
                this.labWorkflowService.RetrieveLabWorkflowById(someRandomId);

            LabWorkflowDependencyException actualLabWorkflowDependencyException =
                await Assert.ThrowsAsync<LabWorkflowDependencyException>(retrieveLabWorkflowByIdTask.AsTask);

            //then
            actualLabWorkflowDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabWorkflowDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveifErrorOccursAndLogItAsync()
        {
            // given
            Guid labWorkflowId = Guid.NewGuid();

            var httpResponseException = new HttpResponseException();

            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(httpResponseException);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyException(failedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(labWorkflowId))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabWorkflow> retrieveLabWorkflowTask =
                this.labWorkflowService.RetrieveLabWorkflowById(labWorkflowId);

            LabWorkflowDependencyException actualLabWorkflowDependencyException =
                await Assert.ThrowsAsync<LabWorkflowDependencyException>(
                    retrieveLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveifBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Guid labWorkflowId = Guid.NewGuid();
            Dictionary<string, List<string>> randomDictionary = CreateRandomDictionary();

            var httpResponseBadRequestException = new HttpResponseBadRequestException();
            httpResponseBadRequestException.AddData(randomDictionary);

            var invalidLabWorkflowException =
                new InvalidLabWorkflowException(httpResponseBadRequestException, randomDictionary);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyValidationException(invalidLabWorkflowException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(labWorkflowId))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<LabWorkflow> retrieveLabWorkflowTask =
                this.labWorkflowService.RetrieveLabWorkflowById(labWorkflowId);

            LabWorkflowDependencyValidationException actualLabWorkflowDependencyException =
                await Assert.ThrowsAsync<LabWorkflowDependencyValidationException>(
                    retrieveLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowDependencyException.Should().BeEquivalentTo(
                expectedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowDependencyException))),
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

            var failedLabWorkflowServiceException =
                new FailedLabWorkflowServiceException(serviceException);

            var expectedLabWorkflowServiceException =
                new LabWorkflowServiceException(failedLabWorkflowServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabWorkflow> getLabWorkflowByIdTask =
                this.labWorkflowService.RetrieveLabWorkflowById(randomId);

            LabWorkflowServiceException actualLabWorkflowServiceException =
                await Assert.ThrowsAsync<LabWorkflowServiceException>(
                    getLabWorkflowByIdTask.AsTask);

            // then
            actualLabWorkflowServiceException.Should().BeEquivalentTo(
                expectedLabWorkflowServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
