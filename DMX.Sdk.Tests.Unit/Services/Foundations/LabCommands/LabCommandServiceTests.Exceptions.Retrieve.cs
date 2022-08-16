﻿// ---------------------------------------------------------------
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
            LabCommand labCommand = CreateRandomLabCommand();
            Guid labCommandId = labCommand.Id;

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
            LabCommand someLabCommand = CreateRandomLabCommand();
            Guid someLabCommandId = someLabCommand.Id;
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpMessage, randomMessage);

            var failedLabCommandDependencyException
                = new FailedLabCommandDependencyException(httpResponseException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyException.Should()
                .BeEquivalentTo(expectedLabCommandDependencyException);

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
            LabCommand someLabCommand = CreateRandomLabCommand();
            Guid someLabCommandId = someLabCommand.Id;
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            var httpBadRequestException =
                new HttpResponseBadRequestException(
                    httpMessage,
                    randomMessage);

            httpBadRequestException.AddData(randomDictionary);

            var invalidPostException =
                new InvalidLabCommandException(
                    httpBadRequestException,
                    randomDictionary);

            var expectedLabCommandDependencyValidationException =
                new LabCommandDependencyValidationException(invalidPostException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandDependencyValidationException actualLabCommandDependencyValidationException =
                await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                    addLabCommandTask.AsTask);

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
    }
}
