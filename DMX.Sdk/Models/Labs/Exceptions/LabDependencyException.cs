// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.Services.Foundation.Labs.Exceptions
{
    public class LabDependencyException : Xeption
    {
        public LabDependencyException(Xeption innerException)
            : base(message: "Lab dependency error occured, contact support.",
                  innerException)
        {
        }
    }
}
