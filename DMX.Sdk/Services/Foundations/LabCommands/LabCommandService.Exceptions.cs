// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using Xeptions;

namespace DMX.Sdk.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private delegate ValueTask<LabCommand> ReturningLabCommandFunction();
        private async ValueTask<LabCommand> TryCatch(ReturningLabCommandFunction returningLabCommandFunction)
        {
            try
            {
                return await returningLabCommandFunction();
            }
            catch (NullLabCommandException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
            }
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var labCommandValidationException = new LabCommandValidationException(xeption);
            this.loggingBroker.LogError(labCommandValidationException);
            return labCommandValidationException;

        }
    }
}
