﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Models.Services.Foundations.Labs.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Sdk.Services.Foundations.Labs
{
    public partial class LabService
    {
        private delegate ValueTask<List<Lab>> ReturningLabsFunction();
        private delegate ValueTask<Lab> ReturningLabFunction();

        private async ValueTask<List<Lab>> TryCatch(ReturningLabsFunction returningLabsFunction)
        {
            try
            {
                return await returningLabsFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabServiceException =
                    new FailedLabServiceException(exception);

                throw CreateAndLogServiceException(failedLabServiceException);
            }
        }

        private async ValueTask<Lab> TryCatch(ReturningLabFunction returningLabFunction)
        {
            try
            {
                return await returningLabFunction();
            }
            catch (NullLabException nullLabException)
            {
                throw CreateAndLogValidationException(nullLabException);
            }
            catch (InvalidLabException invalidLabException)
            {
                throw CreateAndLogValidationException(invalidLabException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidLabException = new AlreadyExistsLabException(
                    httpResponseConflictException,
                    httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidLabException = new InvalidLabException(
                    httpResponseBadRequestException,
                    httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabServiceException =
                    new FailedLabServiceException(exception);

                throw CreateAndLogServiceException(failedLabServiceException);
            }
        }

        private LabDependencyException CreateAndLogCriticalDependencyException(Xeption xeption)
        {
            var labDependencyException = new LabDependencyException(xeption);
            this.loggingBroker.LogCritical(labDependencyException);

            return labDependencyException;
        }

        private LabDependencyException CreateAndLogDependencyException(Xeption xeption)
        {
            var labDependencyException = new LabDependencyException(xeption);
            this.loggingBroker.LogError(labDependencyException);

            return labDependencyException;
        }

        private LabServiceException CreateAndLogServiceException(Xeption xeption)
        {
            var labServiceException = new LabServiceException(xeption);
            this.loggingBroker.LogError(labServiceException);

            return labServiceException;
        }

        private LabDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption xeption)
        {
            var labDependencyValidationException = new LabDependencyValidationException(xeption);
            this.loggingBroker.LogError(labDependencyValidationException);

            return labDependencyValidationException;
        }

        private LabValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var labValidationException = new LabValidationException(xeption);
            this.loggingBroker.LogError(labValidationException);

            return labValidationException;
        }
    }
}
