// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Sdk.Models.LabCommands.Exceptions
{
    public partial class NullLabCommandIdException : Xeption
    {
        public NullLabCommandIdException()
            : base(message: "Lab command id is null.")
        {
        }
    }
}
