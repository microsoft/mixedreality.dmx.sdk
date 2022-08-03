// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Services.Foundations.Labs;

namespace DMX.Sdk.Api.Controllers
{
    public class LabsController
    {
        private readonly ILabService labService;

        public LabsController(ILabService labService) =>
            this.labService = labService;

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            return await this.labService.RetrieveAllLabsAsync();
        }
    }
}
