// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public class AlreadyExistsLabCommandException : Xeption
    {
        public AlreadyExistsLabCommandException(Exception innerException)
            : base(message: "Lab command already exists error occured, contact support",
                  innerException)
        { }

        public AlreadyExistsLabCommandException(Exception innerException, IDictionary data)
            : base(message: "Lab command already exists error occured, contact support",
                innerException,
                data)
        { }
    }
}
