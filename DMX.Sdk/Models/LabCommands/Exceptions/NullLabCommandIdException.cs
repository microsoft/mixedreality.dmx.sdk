// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public class NullLabCommandIdException : Xeption
    {
        public NullLabCommandIdException()
            : base(message:"Null Lab command id error occurred. Please try again.")
        {
        }
    }
}
