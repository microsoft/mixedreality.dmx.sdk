﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

namespace DMX.Sdk.Models.Services.Foundations.Labs
{
    public class LabDevice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? PowerLevel { get; set; }
        public LabDeviceType Type { get; set; }
        public LabDeviceStatus Status { get; set; }
        public LabDeviceCategory Category { get; set; }
    }
}
