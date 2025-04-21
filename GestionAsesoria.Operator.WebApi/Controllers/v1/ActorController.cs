using GestionAsesoria.Operator.Application.DTOs.Actor.Response;
using GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup;
using GestionAsesoria.Operator.Application.Features.Actors.Queries.GetActorsByMainRole;
using GestionAsesoria.Operator.Application.Features.Actors.Queries.GetSelect;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using GestionAsesoria.Operator.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    public class ActorController : BaseApiController<ActorController>
    {
        /// <summary>
        /// Obtiene los grupos de investigación activos.
        /// </summary>
        [Authorize(Policy = Permissions.Actors.View)]
        [HttpGet("GetResearchGroups")]
        public async Task<ActionResult<Result<IEnumerable<GetActorResearchGroupDto>>>> GetResearchGroups()
        {
            var result = await _mediator.Send(new GetActorsByMainRoleQuery());

            if (!result.Succeeded)
                return BadRequest(result); // Devuelve 400 si hay error

            return Ok(result); // Devuelve 200 con los datos si es exitoso
        }

        [Authorize(Policy = Permissions.Actors.View)]
        [HttpGet("GetActorsByParent/{actorResearchGroupId}/{roleId}")]
        public async Task<ActionResult<Result<IEnumerable<ActorResponseDto>>>> GetActorsByParentAndRole(int actorResearchGroupId, int roleId)
        {
            try
            {
                // Ejecutar el Query para obtener actores por grupo y rol
                var result = await _mediator.Send(new GetChildActorsByParentAndRoleQuery
                {
                    ParentId = actorResearchGroupId,
                    RoleId = roleId
                });

                if (!result.Succeeded)
                    return BadRequest(result); // Devuelve 400 si hubo un error en el Query

                return Ok(result); // Devuelve 200 con los datos si es exitoso
            }
            catch (Exception ex)
            {
                // Manejo de errores y excepciones
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }


        }
    }
}