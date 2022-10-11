// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowDependencyException : Xeption
    {
        public FailedLabWorkflowDependencyException(Exception innerException)
            : base(message: "Failed lab workflow error occurred. Please contact support.",
                 innerException)
        { }
    }
}
