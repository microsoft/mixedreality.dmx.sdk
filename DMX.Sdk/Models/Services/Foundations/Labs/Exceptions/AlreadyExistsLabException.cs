﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Sdk.Models.Services.Foundations.Labs.Exceptions
{
    public class AlreadyExistsLabException : Xeption
    {
        public AlreadyExistsLabException()
            : base(message: "Invalid lab error occurred, please correct the errors and try again.")
        { }

        public AlreadyExistsLabException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab error occurred, please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
