// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabWorkflows;

namespace DMX.Sdk.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabWorkflow> PostLabWorkflowAsync(LabWorkflow labWorkflow);
        ValueTask<LabWorkflow> GetLabWorkflowByIdAsync(Guid id);
    }
}
