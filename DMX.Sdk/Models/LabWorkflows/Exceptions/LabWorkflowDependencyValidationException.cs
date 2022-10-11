// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowDependencyValidationException : Xeption
    {
        public LabWorkflowDependencyValidationException(Xeption innerException)
            : base(message: "Lab workflow dependency validation error occurred. Please contact support.",
                  innerException)
        { }
    }
}
