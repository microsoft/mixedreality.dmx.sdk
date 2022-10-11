// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowDependencyException : Xeption
    {
        public LabWorkflowDependencyException(Xeption innerException)
            : base(message: "Lab workflow dependency error occurred. Please contact support.",
                 innerException)
        { }
    }
}
