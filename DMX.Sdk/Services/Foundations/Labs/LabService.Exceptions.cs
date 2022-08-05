// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundation.Exceptions;
using DMX.Sdk.Models.Services.Foundation.Labs.Exceptions;
using DMX.Sdk.Models.Services.Foundations.Labs;
using RESTFulSense.Exceptions;

namespace DMX.Sdk.Services.Foundations.Labs
{
    public partial class LabService
    {
        private delegate ValueTask<List<Lab>> ReturningLabsFunction();

        private async ValueTask<List<Lab>> TryCatch(ReturningLabsFunction returningLabsFunction)
        {
            try
            {
                return await returningLabsFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseUrlNotFoundException);
                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseUnauthorizedException);
                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseForbiddenException);
                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
        }

        private LabDependencyException CreateAndLogCriticalDependencyException(
            FailedLabDependencyException failedLabDependencyException)
        {
            var labDependencyException = new LabDependencyException(failedLabDependencyException);
            this.loggingBroker.LogCritical(labDependencyException);

            return labDependencyException;
        }
    }
}
