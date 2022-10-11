// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class AlreadyExistsLabWorkflowException : Xeption
    {
        public AlreadyExistsLabWorkflowException(Exception exception)
            : base(message: "Lab workflow already exists. Please try again.",
                 exception)
        { }

        public AlreadyExistsLabWorkflowException(Exception exception, IDictionary data)
            : base(message: "Lab workflow already exists. Please try again.",
                 exception,
                 data)
        { }
    }
}
