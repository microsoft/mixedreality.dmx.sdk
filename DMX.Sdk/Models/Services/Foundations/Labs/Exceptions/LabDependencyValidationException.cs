﻿using Xeptions;

namespace DMX.Sdk.Models.Services.Foundations.Labs.Exceptions
{
    public class LabDependencyValidationException : Xeption
    {
        public LabDependencyValidationException(Xeption exception)
            : base(message: "Lab dependency validation error occurred, please try again.",
                  innerException: exception)
        { }
    }
}
