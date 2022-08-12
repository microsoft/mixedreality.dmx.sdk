﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Moq;
using Xunit;
using Xeptions;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using DMX.Sdk.Models.Services.Foundation.Labs.Exceptions;
using FluentAssertions;
using RESTFulSense.Exceptions;

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
                await Assert.ThrowsAsync<LabCommandDependencyException>(returnedLabCommand.AsTask);

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
    }
}