// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;
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
        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption nullLabWorkflowException)
        {
            var labWorkflowValidationException = new LabWorkflowValidationException(nullLabWorkflowException);
            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }
    }
}
