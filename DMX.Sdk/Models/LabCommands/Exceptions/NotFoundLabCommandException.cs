// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public class NotFoundLabCommandException : Xeption
    {
        public NotFoundLabCommandException()
            : base(message:"Lab command not found. Please try again.")
        {
        }
    }
}
