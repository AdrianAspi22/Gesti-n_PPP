using GestionAsesoria.Operator.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GestionAsesoria.Operator.Shared.Constants.Permission;

namespace GestionAsesoria.Operator.WebApi.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;

        public AuditsController(ICurrentUserService currentUserService, IAuditService auditService)
        {
            _currentUserService = currentUserService;
            _auditService = auditService;
        }

        /// <summary>
        /// Get Current User Audit Trails
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.AuditTrails.View)]
        [HttpGet]
        public async Task<IActionResult> GetUserTrailsAsync()
        {
            var response = await _auditService.GetCurrentUserTrailsAsync(_currentUserService.UserId);
            return Ok(response);
        }

       
    }
}