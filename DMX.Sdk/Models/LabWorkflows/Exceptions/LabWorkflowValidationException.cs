// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace DMX.Sdk.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowValidationException : Xeption
    {
        public LabWorkflowValidationException(Xeption innerException)
            : base(message: "Lab workflow validation error occured. Please fix and try again.",
                  innerException)
        { }
    }
}
