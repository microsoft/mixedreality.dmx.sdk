// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundation.Exceptions;
using DMX.Sdk.Models.Services.Foundation.Labs.Exceptions;
using DMX.Sdk.Models.Services.Foundations.Labs;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
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
            var failedLabDependencyException =
                new FailedLabDependencyException(criticalDependencyException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBroker
                .Setup(broker => broker.GetAllLabsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Lab>> retrieveAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabDependencyException actualLabDependencyException = 
                await Assert.ThrowsAsync<LabDependencyException>(retrieveAllLabsTask.AsTask);

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBroker.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfErrorOccursLogItAsync()
        {
            // given
            string someMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(responseMessage, someMessage);

            var failedLabDependencyException =
                new FailedLabDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBroker
                .Setup(broker => broker.GetAllLabsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Lab>> retrieveAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(retrieveAllLabsTask.AsTask);

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBroker.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBroker.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrievalIfErrorOccursAndLogItAsync()
        {
            // given
            string someMessage = GetRandomString();
            var serviceException = new Exception();

            var failedLabServiceException =
                new FailedLabServiceException(serviceException);

            var expectedLabServiceException =
                new LabServiceException(failedLabServiceException);

            this.dmxApiBroker
                .Setup(broker => broker.GetAllLabsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Lab>> retrieveAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabServiceException actualLabServiceException =
                await Assert.ThrowsAsync<LabServiceException>(retrieveAllLabsTask.AsTask);

            // then
            actualLabServiceException
                .Should().BeEquivalentTo(expectedLabServiceException);

            this.dmxApiBroker.Verify(broker => 
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBroker.Verify(broker => 
                broker.LogError(
                    It.Is(SameExceptionAs(expectedLabServiceException))),
                        Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}
