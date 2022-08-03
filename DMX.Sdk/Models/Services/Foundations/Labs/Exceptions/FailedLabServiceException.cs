﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Sdk.Models.Services.Foundations.Labs.Exceptions
{
    public class FailedLabServiceException : Xeption
    {
        public FailedLabServiceException(Exception innerException)
            : base(message: "Failed lab service error occurred, contact support.",
                  innerException)
        {
        }
    }
}
