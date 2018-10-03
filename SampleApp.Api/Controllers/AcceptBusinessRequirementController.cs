using SampleApp.Api.Helpers;
using SampleApp.Api.Models;
using SampleApp.Entities;
using SampleApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace SampleApp.Api.Controllers
{
    public class AcceptBusinessRequirementController : BaseApiController
    {
        private readonly ILawAcceptanceService _lawAcceptanceService;

        public AcceptBusinessRequirementController()
        {

        }

        public AcceptBusinessRequirementController(ILawAcceptanceService lawAcceptanceService)
        {
            _lawAcceptanceService = lawAcceptanceService;
        }

        public async Task<IHttpActionResult> PostAcceptBusinessRequirement(AcceptBusinessRequirement accept)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid parameters");
            }

            Respose<bool> respose = new Respose<bool>();

            try
            {
                //var hasRequirements = await _checkDataService.CheckRequirements(accept);
                //var isProcessMapped = await _checkDataService.CheckProcessMapping(accept);
                //if (!hasRequirements)
                //{
                //    ConflictResponseRequirement(accept, respose);
                //    return new NegotiatedContentResult<Respose<bool>>(HttpStatusCode.Conflict, respose, this);
                //}

                //if (!isProcessMapped)
                //{
                //    ConflictResponseProcessMapping(accept, respose);
                //    return new NegotiatedContentResult<Respose<bool>>(HttpStatusCode.Conflict, respose, this);
                //}
                accept.UserId = UserId;
                accept.UserName = UserName;
                var result = await _lawAcceptanceService.AcceptBusinessRequirement(accept);
                if (result)
                {
                    LawAcceptanceSuccessResponse(respose);
                    return new NegotiatedContentResult<Respose<bool>>(HttpStatusCode.OK, respose, this);
                }
                else
                {
                    LawAcceptanceFailureResponse(respose);
                    return new NegotiatedContentResult<Respose<bool>>(HttpStatusCode.ExpectationFailed, respose, this);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        #region Private Method
        private void LawAcceptanceFailureResponse(Respose<bool> respose)
        {
            respose.Result = false;
            respose.Message = "Can't accept law at the moment. Please try again later";
        }

        private void LawAcceptanceSuccessResponse(Respose<bool> respose)
        {
            respose.Result = true;
            respose.Message = "Law accepted successfully.";
        }

        private void ConflictResponseProcessMapping(AcceptBusinessRequirement acceptBusinessReqs, Respose<bool> respose)
        {
            respose.Result = false;
            respose.Message = "No process mapped. Contact system administrator.";
        }

        private void ConflictResponseRequirement(AcceptBusinessRequirement acceptBusinessReqs, Respose<bool> respose)
        {
            respose.Result = false;
            respose.Message = "Selected law does not have any requirement.";
        }
        #endregion
    }
}
