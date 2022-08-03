// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

namespace DMX.Sdk.Models.Services.Foundations.Artifacts
{
    public class Artifact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public byte[] Data { get; set; }
    }
}
