// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabWorkflowIsNullAndLogItAsync()
        {
            // given
            LabWorkflow nullLabWorkflow = null;
            var nullLabWorkflowException = new NullLabWorkflowException();

            var expectedLabWorkflowValidationException = 
                new LabWorkflowValidationException(nullLabWorkflowException);

            // when
            ValueTask<LabWorkflow> addLabWorkflowTask =
                this.labWorkflowService.AddLabWorkflowAsync(nullLabWorkflow);

            LabWorkflowValidationException actualLabWorkflowValidationException =
                await Assert.ThrowsAsync<LabWorkflowValidationException>(
                    addLabWorkflowTask.AsTask);

            // then
            actualLabWorkflowValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabWorkflowAsync(It.IsAny<LabWorkflow>()),
                    Times.Never());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
