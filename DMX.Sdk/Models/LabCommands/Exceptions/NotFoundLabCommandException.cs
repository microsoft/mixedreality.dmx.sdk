// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public class NotFoundLabCommandException : Xeption
    {
        public NotFoundLabCommandException(Guid id)
            : base(message:$"Lab command not found for {id}. Please try again.")
        {
        }
    }
}
