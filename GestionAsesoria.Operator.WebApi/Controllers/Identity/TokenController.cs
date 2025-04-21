using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.Identity
{
    [Route("api/identity")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _identityService;

        public TokenController(ITokenService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
          
        }

        /// <summary>
        /// Get Token (User, Password)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Get(TokenRequest model)
        {
           
           var response = await _identityService.LoginAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [AllowAnonymous]
        [HttpPost("refreshToken")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            var response = await _identityService.GetRefreshTokenAsync(model);
            return Ok(response);
        }
    }
}