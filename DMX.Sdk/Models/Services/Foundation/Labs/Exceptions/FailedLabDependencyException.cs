// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.Services.Foundation.Exceptions
{
    public class FailedLabDependencyException : Xeption
    {
        public FailedLabDependencyException(Exception innerException)
            : base(message: "Failed lab dependency error occured, contact support.",
                  innerException)
        {
        }
    }
}
