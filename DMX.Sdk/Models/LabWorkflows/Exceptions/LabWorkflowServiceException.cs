// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowServiceException : Xeption
    {
        public LabWorkflowServiceException(Xeption innerException)
            : base(message: "Lab workflow service error occured. Please contact support.",
                  innerException)
        { }
    }
}
