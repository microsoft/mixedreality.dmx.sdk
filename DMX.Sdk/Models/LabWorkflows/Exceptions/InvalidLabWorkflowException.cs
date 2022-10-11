// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class InvalidLabWorkflowException : Xeption
    {
        public InvalidLabWorkflowException(Exception innerException)
            : base(message: "Invalid lab workflow error occured. Please fix and try again.",
                  innerException)
        {
        }

        public InvalidLabWorkflowException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab workflow error occured. Please fix and try again.",
                  innerException,
                  data)
        {
        }
    }
}
