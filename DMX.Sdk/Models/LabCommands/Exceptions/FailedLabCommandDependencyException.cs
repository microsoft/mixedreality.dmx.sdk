// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public class FailedLabCommandDependencyException : Xeption
    {
        public FailedLabCommandDependencyException(Xeption innerException)
            : base(message: "Failed lab command dependency exception occured. Please contact support.", innerException)
        { }
    }
}
