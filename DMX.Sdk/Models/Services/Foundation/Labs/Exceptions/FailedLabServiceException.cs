// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.Services.Foundation.Labs.Exceptions
{
    public class FailedLabServiceException : Xeption
    {
        public FailedLabServiceException(Exception innerException)
            : base(message: "Failed lab service error occured, contact support.",
                  innerException)
        {
        }
    }
}
