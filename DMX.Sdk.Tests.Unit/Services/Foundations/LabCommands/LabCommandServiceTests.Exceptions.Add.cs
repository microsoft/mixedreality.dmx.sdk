﻿// ---------------------------------------------------------------
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
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            LabCommand labCommand = CreateRandomLabCommand();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(criticalDependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<LabCommand> returnedLabCommand =
                this.labCommandService.AddLabCommandAsync(labCommand);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    returnedLabCommand.AsTask);

            // then
            actualLabCommandDependencyException
                .Should().BeEquivalentTo(expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(labCommand),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(
                    It.Is(SameExceptionAs(expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            LabCommand someLabCommand = CreateRandomLabCommand();
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpMessage, randomMessage);

            var failedLabCommandDependencyException
                = new FailedLabCommandDependencyException(httpResponseException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(someLabCommand);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyException.Should()
                .BeEquivalentTo(expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            LabCommand someLabCommand = CreateRandomLabCommand();
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
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(someLabCommand);

            LabCommandDependencyValidationException actualLabCommandDependencyValidationException =
                await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyValidationException.Should()
                .BeEquivalentTo(expectedLabCommandDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfLabCommandAlreadyExistsOccursAndLogItAsync()
        {
            // given
            LabCommand randomCommand = CreateRandomLabCommand();
            string randomString = GetRandomString();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            var httpResponseConflictException = new HttpResponseConflictException(
                httpResponseMessage,
                randomString);

            httpResponseConflictException.AddData(randomDictionary);

            var alreadyExistsLabCommandException =
                new AlreadyExistsLabCommandException(
                    httpResponseConflictException,
                    randomDictionary);

            var expectedLabCommandDependencyValidationException =
                new LabCommandDependencyValidationException(alreadyExistsLabCommandException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(httpResponseConflictException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(randomCommand);

            LabCommandDependencyValidationException actualLabCommandDependencyValidationException =
                await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyValidationException.Should().BeEquivalentTo(
                expectedLabCommandDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                   Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            LabCommand randomCommand = CreateRandomLabCommand();
            var serviceException = new Exception();

            var failedLabCommandServiceException =
                new FailedLabCommandServiceException(serviceException);

            var expectedLabCommandServiceException =
                new LabCommandServiceException(failedLabCommandServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(randomCommand);

            LabCommandServiceException actualLabCommandServiceException =
                await Assert.ThrowsAsync<LabCommandServiceException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandServiceException.Should().BeEquivalentTo(
                expectedLabCommandServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
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
