using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Commands.Create;
using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Commands.Delete;
using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetAllAdvisoringContracts;
using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetByIdAdvisoringContracts;
using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetSelectAdvisorinigContracts;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    public class AdvisoringContractController : BaseApiController<AdvisoringContractController>
    {
        #region
        /// <summary>
        /// Create AdvisoringContract
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.AdvisoringContracts.Create)]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(CreateAdvisoringContractCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //[Authorize(Policy = Permissions.AdvisoringContracts.Edit)]
        //[HttpPut("Update")]
        //public async Task<IActionResult> Update(UpdateAdvisoringContractCommand command)
        //{
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}

        /// <summary>
        /// Delete AdvisoringContract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.AdvisoringContracts.Delete)]
        [HttpDelete("Delete/{AdvisoringContractId:int}")]
        public async Task<IActionResult> Delete(int contractId)
        {
            var response = await _mediator.Send(new DeleteAdvisoringContractCommand { AdvisoringContractId = contractId });
            return Ok(response);
        }
        #endregion

        #region
        /// <summary>
        /// Get All AdvisoringContracts
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = Permissions.AdvisoringContracts.View)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAdvisoringContractParameters parameters)
        {
            var query = new GetAllAdvisoringContractQuery { parameters = parameters };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// GetContractById
        /// </summary>
        /// <param name="advisoringContractId"></param>
        /// <returns></returns>
        [HttpGet("GetContractById/{advisoringContractId:int}")]
        [Authorize(Policy = Permissions.AdvisoringContracts.View)]
        public async Task<IActionResult> GetContractById(int advisoringContractId)
        {
            var contract = await _mediator.Send(new GetAdvisoringContractByIdQuery() { advisoringContractId = advisoringContractId });
            return Ok(contract);
        }

        /// <summary>
        /// Get Select AdvisoringContract
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.AdvisoringContracts.View)]
        [HttpGet("GetSelectAdvisoringContract")]
        public async Task<IActionResult> GetSelectAdvisoringContract()
        {
            var response = await _mediator.Send(new GetSelectAdvisoringContractQuery());
            return Ok(response);
        }
        #endregion

    }
}
