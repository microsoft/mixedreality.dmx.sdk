// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;

namespace DMX.Sdk.Services.Foundations.LabWorkflows
{
    public interface ILabWorkflowService
    {
        ValueTask<LabWorkflow> AddLabWorkflowAsync(LabWorkflow labWorkflow);
        ValueTask<LabWorkflow> RetrieveLabWorkflowById(Guid labWorkflowId);
    }
}
