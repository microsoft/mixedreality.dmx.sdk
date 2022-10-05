// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Sdk.Models.LabCommands;

namespace DMX.Sdk.Models.LabWorkflows
{
    public class LabWorkflow
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public List<LabCommand> Commands { get; set; }
        public WorkflowStatus Status { get; set; }
        public string Notes { get; set; }
        public ulong CreatedBy { get; set; }
        public ulong UpdatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Results { get; set; }
    }
}
