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
    public class NullLabWorkflowException : Xeption
    {
        public NullLabWorkflowException()
            : base(message: "Lab workflow is null.")
        {
        }
    }
}
