// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowServiceException : Xeption
    {
        public FailedLabWorkflowServiceException(Exception innerException)
            : base(message: "Failed lab workflow error occured. Please contact support.",
                  innerException)
        { }
    }
}
