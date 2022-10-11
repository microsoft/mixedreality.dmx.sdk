// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Sdk.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {
        private delegate ValueTask<LabWorkflow> ReturningLabWorkflowFunction();

        private async ValueTask<LabWorkflow> TryCatch(ReturningLabWorkflowFunction returningLabWorkflowFunction)
        {
            try
            {
                return await returningLabWorkflowFunction();
            }
            catch (NullLabWorkflowException nullLabWorkflowException)
            {
                throw CreateAndLogValidationException(nullLabWorkflowException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                throw CreateAndLogCriticalDependencyException(httpResponseUrlNotFoundException);

            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                throw CreateAndLogCriticalDependencyException(httpResponseUnauthorizedException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                throw CreateAndLogCriticalDependencyException(httpResponseForbiddenException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabWorkflowDependencyException
                    = new FailedLabWorkflowDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabWorkflowDependencyException);
            }
        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption nullLabWorkflowException)
        {
            var labWorkflowValidationException = new LabWorkflowValidationException(nullLabWorkflowException);
            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }

        private LabWorkflowDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var failedLabWorkflowDependencyException =
                new FailedLabWorkflowDependencyException(exception);

            var labWorkflowDependencyException = new LabWorkflowDependencyException(failedLabWorkflowDependencyException);
            this.loggingBroker.LogCritical(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labWorkflowDependencyException = new LabWorkflowDependencyException(exception);
            this.loggingBroker.LogError(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }
    }
}
