// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Models.LabCommands.Exceptions;
using RESTFulSense.Exceptions;
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
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidLabCommandException =
                    new InvalidLabCommandException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabCommandException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsLabCommandException =
                    new AlreadyExistsLabCommandException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(alreadyExistsLabCommandException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabCommandDependencyException);
            }
            catch (NullLabCommandException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
            }
            catch (Exception exception)
            {
                var failedLabCommandServiceException =
                    new FailedLabCommandServiceException(exception);

                throw CreateAndLogServiceException(failedLabCommandServiceException);
            }
        }

        private Exception CreateAndLogDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogError(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private Exception CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogCritical(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private LabCommandDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var labCommandDependencyValidationException =
                new LabCommandDependencyValidationException(exception);

            this.loggingBroker.LogError(labCommandDependencyValidationException);

            return labCommandDependencyValidationException;
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labCommandValidationException = new LabCommandValidationException(exception);
            this.loggingBroker.LogError(labCommandValidationException);

            return labCommandValidationException;

        }

        private LabCommandServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labCommandServiceException =
                new LabCommandServiceException(exception);

            this.loggingBroker.LogError(labCommandServiceException);

            return labCommandServiceException;
        }
    }
}
