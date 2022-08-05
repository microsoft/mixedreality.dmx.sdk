// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.Services.Foundation.Labs.Exceptions
{
    public class LabServiceException : Xeption
    {
        public LabServiceException(Exception innerException)
            : base(message: "Lab service exception occured, contact support.",
                  innerException)
        {
        }
    }
}
