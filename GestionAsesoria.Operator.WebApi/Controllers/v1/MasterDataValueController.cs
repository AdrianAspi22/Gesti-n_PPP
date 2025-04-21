using GestionAsesoria.Operator.Application.Features.MasterDataValues.Queries.GetSelect;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    public class MasterDataValueController : BaseApiController<MasterDataValueController>
    {
        /// <summary>
        /// Get Select MasterDataValue
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.MasterDataValues.View)]
        [HttpGet("GetSelectMasterDataValue")]
        public async Task<IActionResult> GetSelectMasterDataValue()
        {
            var response = await _mediator.Send(new GetSelectMasterDataValueQuery());
            return Ok(response);
        }
    }
}
