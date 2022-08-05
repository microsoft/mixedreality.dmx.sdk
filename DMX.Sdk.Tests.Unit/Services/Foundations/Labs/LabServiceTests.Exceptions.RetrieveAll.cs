// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundation.Exceptions;
using DMX.Sdk.Models.Services.Foundation.Labs.Exceptions;
using DMX.Sdk.Models.Services.Foundations.Labs;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Theory]
        [MemberData (nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalErrorOccursLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var failedLabDependencyException = new FailedLabDependencyException(criticalDependencyException);
            var expectedLabDependencyException = new LabDependencyException(failedLabDependencyException);

            this.dmxApiBroker
                .Setup(broker => broker.GetAllLabsAsync())
                .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Lab>> getAllLabsTask = this.labService.RetrieveAllLabsAsync();

            LabDependencyException actualLabDependencyException = 
                await Assert.ThrowsAsync<LabDependencyException>(getAllLabsTask.AsTask);

            // then
            actualLabDependencyException.Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBroker
                .Verify(broker => broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBroker
                .Verify(broker => broker.LogCritical(
                    It.Is(SameExceptionAs(expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();

        }
    }
}
