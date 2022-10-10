// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using Xeptions;
using Xunit;
using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;
using FluentAssertions;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            LabWorkflow labWorkflow = CreateRandomLabWorkflow();

            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(criticalDependencyException);

            var expectedLabWorkflowDependencyException =
                new LabWorkflowDependencyException(failedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabWorkflowAsync(It.IsAny<LabWorkflow>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<LabWorkflow> returnedLabWorkflow =
                this.labWorkflowService.AddLabWorkflowAsync(labWorkflow);

            LabWorkflowDependencyException actualLabWorkflowDependencyException =
                await Assert.ThrowsAsync<LabWorkflowDependencyException>(
                    returnedLabWorkflow.AsTask);

            // then
            actualLabWorkflowDependencyException
                .Should().BeEquivalentTo(expectedLabWorkflowDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabWorkflowAsync(labWorkflow),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedLabWorkflowDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
