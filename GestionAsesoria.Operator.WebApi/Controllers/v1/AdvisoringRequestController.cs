using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Commands.Create;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Commands.Delete;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetAllAdvisoringRequests;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetByIdAdvisoringRequests;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetSelectAdvisoringRequest;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    public class AdvisoringRequestController : BaseApiController<AdvisoringRequestController>
    {
        #region
        /// <summary>
        /// Respond to AdvisoringRequest
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.AdvisoringRequests.Create)]
        [HttpPost("Respond")]
        public async Task<IActionResult> Respond(RespondToAdvisoringContractCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete AdvisoringRequest
        /// </summary>
        /// <param name="advisoringRequestId"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.AdvisoringRequests.Delete)]
        [HttpDelete("Delete/{AdvisoringRequestId:int}")]
        public async Task<IActionResult> Delete(int advisoringRequestId)
        {
            var response = await _mediator.Send(new DeleteAdvisoringRequestCommand { AdvisoringRequestId = advisoringRequestId });
            return Ok(response);
        }
        #endregion

        #region
        /// <summary>
        /// Get All AdvisoringRequests
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.AdvisoringRequests.View)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAdvisoringRequestParameters parameters)
        {
            var query = new GetAllAdvisoringRequestQuery { parameters = parameters };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// GetAdvisoringRequestById
        /// </summary>
        /// <param name="advisoringRequestId"></param>
        /// <returns></returns>
        [HttpGet("GetContractById/{advisoringRequestId:int}")]
        [Authorize(Policy = Permissions.AdvisoringRequests.View)]
        public async Task<IActionResult> GetCAdvisoringRequestById(int advisoringRequestId)
        {
            var contract = await _mediator.Send(new GetAdvisoringRequestByIdQuery() { advisoringRequestId = advisoringRequestId });
            return Ok(contract);
        }

        /// <summary>
        /// Get Select AdvisoringRequest
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.AdvisoringRequests.View)]
        [HttpGet("GetSelectAdvisoringRequest")]
        public async Task<IActionResult> GetSelectAdvisoringRequest()
        {
            var response = await _mediator.Send(new GetSelectAdvisoringRequestQuery());
            return Ok(response);
        }
        #endregion

    }
}
