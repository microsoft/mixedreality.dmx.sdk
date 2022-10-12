// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Models.LabWorkflows.Exceptions;

namespace DMX.Sdk.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {
        public void ValidateLabWorkflow(LabWorkflow labWorkflow)
        {
            if (labWorkflow is null)
            {
                throw new NullLabWorkflowException();
            }
        }

        public void ValidateLabWorkflowId(Guid labWorkflowId)
        {
            if (labWorkflowId == Guid.Empty)
            {
                throw new NullLabWorkflowException();
            }
        }
    }
}
