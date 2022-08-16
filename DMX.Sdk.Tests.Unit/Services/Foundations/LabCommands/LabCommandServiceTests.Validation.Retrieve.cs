// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfLabCommandIdIsInvalidAndLogItAsync()
        {
            // given
            Guid emptyId = Guid.Empty;
            var nullLabCommandIdException = new NullLabCommandIdException();

            nullLabCommandIdException.AddData(
                key: nameof(LabCommand.Id),
                values: "Id is required");

            var expectedLabCommandValidationException =
                new LabCommandValidationException(nullLabCommandIdException);

            // when
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(emptyId);

            LabCommandValidationException actualLabCommandValidationException =
                await Assert.ThrowsAsync<LabCommandValidationException>(
                    retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandValidationException.Should().BeEquivalentTo(
                expectedLabCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
