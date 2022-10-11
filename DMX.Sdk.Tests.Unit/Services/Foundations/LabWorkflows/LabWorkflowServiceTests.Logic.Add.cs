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
        public async Task ShouldAddLabWorkflowAsync()
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;
            LabWorkflow returnedLabWorkflow = inputLabWorkflow;
            LabWorkflow expectedLabWorkflow = inputLabWorkflow.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabWorkflowAsync(inputLabWorkflow))
                    .ReturnsAsync(returnedLabWorkflow);

            // when
            LabWorkflow actualLabWorkflow = 
                await this.labWorkflowService.AddLabWorkflowAsync(inputLabWorkflow);

            // then
            actualLabWorkflow.Should().BeEquivalentTo(expectedLabWorkflow);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabWorkflowAsync(inputLabWorkflow),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
