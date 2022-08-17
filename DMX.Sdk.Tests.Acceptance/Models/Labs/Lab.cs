// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

namespace DMX.Sdk.Tests.Acceptance.Models.Labs
{
    public class Lab
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LabStatus Status { get; set; }
        public List<LabDevice> Devices { get; set; }
    }
}
