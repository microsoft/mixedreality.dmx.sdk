﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Sdk.Models.Services.Foundations.Labs.Exceptions
{
    public class FailedLabDependencyException : Xeption
    {
        public FailedLabDependencyException(Exception innerException)
            : base(message: "Failed lab dependency error occurred, contact support.",
                  innerException)
        {
        }
    }
}
