// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------


namespace DMX.Sdk.Models.Configurations
{
    public class DownstreamApiConfiguration
    {
        public string BaseUrl { get; set; }
        public IDictionary<string, string> Scopes { get; set; }
    }
}
