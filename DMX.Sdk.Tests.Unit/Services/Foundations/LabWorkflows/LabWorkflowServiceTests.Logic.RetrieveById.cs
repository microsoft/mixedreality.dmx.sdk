// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveLabWorkflowByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputId = Guid.NewGuid();
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow retrievedLabWorkflow = randomLabWorkflow;
            LabWorkflow expectedLabWorkflow = retrievedLabWorkflow.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(inputId))
                    .ReturnsAsync(retrievedLabWorkflow);

            // when
            LabWorkflow actualLabWorkflow =
                await this.labWorkflowService.RetrieveLabWorkflowById(inputId);

            // then
            actualLabWorkflow.Should().BeEquivalentTo(expectedLabWorkflow);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(inputId),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
