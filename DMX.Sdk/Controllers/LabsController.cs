﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Models.Services.Foundations.Labs.Exceptions;
using DMX.Sdk.Services.Foundations.Labs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

#if RELEASE
using Microsoft.Identity.Web.Resource;
#endif

namespace DMX.Sdk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabsController : RESTFulController
    {
        private readonly ILabService labService;

        public LabsController(ILabService labService) =>
            this.labService = labService;

        [HttpGet]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:GetAllLabs")]
#endif
        public async ValueTask<ActionResult<List<Lab>>> GetAllLabsAsync()
        {
            try
            {
                List<Lab> allLabs =
                    await this.labService.RetrieveAllLabsAsync();

                return Ok(allLabs);
            }
            catch (LabDependencyException labDependencyException)
            {
                return InternalServerError(labDependencyException);
            }
            catch (LabServiceException labServiceException)
            {
                return InternalServerError(labServiceException);
            }
        }

        [HttpPost]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLab")]
#endif
        public async ValueTask<ActionResult<Lab>> PostLabAsync(Lab lab)
        {
            try
            {
                Lab addLab =
                    await this.labService.AddLabAsync(lab);

                return Created(addLab);
            }
            catch (LabValidationException labValidationException)
            {
                return BadRequest(labValidationException.InnerException);
            }
            catch (LabDependencyException labDependencyException)
            {
                return InternalServerError(labDependencyException);
            }
            catch (LabDependencyValidationException labDependencyValidationException)
                when (labDependencyValidationException.InnerException is AlreadyExistsLabException)
            {
                return Conflict(labDependencyValidationException.InnerException);
            }
            catch (LabDependencyValidationException labDependencyValidationException)
            {
                return BadRequest(labDependencyValidationException.InnerException);
            }
            catch (LabServiceException labServiceException)
            {
                return InternalServerError(labServiceException);
            }
        }
    }
}
