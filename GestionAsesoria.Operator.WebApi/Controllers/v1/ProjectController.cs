using GestionAsesoria.Operator.Application.Features.Projects.Commands.Create;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseApiController<ProjectController>
    {
        /// <summary>
        /// Create ResearchArea
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Projects.Create)]
        [HttpPost("Create")]
        public async Task<IActionResult> Post(CreateProjectCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
