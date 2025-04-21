using GestionAsesoria.Operator.Application.Configurations;
using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using GestionAsesoria.Operator.Application.Interfaces.Services.Identity;
using GestionAsesoria.Operator.Domain.Entities.Identity;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Services.Identity
{
    public class IdentityService : ITokenService
    {
        private const string InvalidErrorMessage = "Invalid username or password.";

        private readonly UserManager<AcademicUser> _userManager;
        private readonly RoleManager<AcademicRole> _roleManager;
        private readonly AppConfiguration _appConfig;
        private readonly SignInManager<AcademicUser> _signInManager;

        public IdentityService(
            UserManager<AcademicUser> userManager, RoleManager<AcademicRole> roleManager,
            IOptions<AppConfiguration> appConfig, SignInManager<AcademicUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appConfig = appConfig.Value;
            _signInManager = signInManager;
        }

        public async Task<Result<TokenResponse>> LoginAsync(TokenRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName!);
            if (user == null)
            {
                return await Result<TokenResponse>.FailAsync("User Not Found.");
            }
            if (!user.IsActive)
            {
                return await Result<TokenResponse>.FailAsync("User Not Active. Please contact the administrator.");
            }
            if (!user.EmailConfirmed)
            {
                return await Result<TokenResponse>.FailAsync("E-Mail not confirmed.");
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password!);
            if (!passwordValid)
            {
                return await Result<TokenResponse>.FailAsync("Invalid Credentials.");
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            var token = await GenerateJwtAsync(user);
            var response = new TokenResponse { Token = token, RefreshToken = user.RefreshToken = user.ProfilePictureDataUrl, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
            return await Result<TokenResponse>.SuccessAsync(response, ReplyMessage.MESSAGE_TOKEN);
        }

        public async Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model)
        {
            if (model is null)
            {
                return await Result<TokenResponse>.FailAsync("Invalid Client Token.");
            }
            var userPrincipal = GetPrincipalFromExpiredToken(model.Token!);
            var userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email!);
            var user = await _userManager.FindByEmailAsync(userEmail!);
            if (user == null)
                return await Result<TokenResponse>.FailAsync("User Not Found.");
            if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return await Result<TokenResponse>.FailAsync("Invalid Client Token.");
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            user.RefreshToken = GenerateRefreshToken();
            await _userManager.UpdateAsync(user);

            var response = new TokenResponse { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        private async Task<string> GenerateJwtAsync(AcademicUser user)
        {
            var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            return token;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(AcademicUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                var thisRole = await _roleManager.FindByNameAsync(role);
                var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole!);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.UserData, user.UserName!),
                //new(ClaimTypes.Name, user.FirstName!),
                //new(ClaimTypes.Surname, actorType.tostring()),
                new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                new(ClaimTypes.Actor, user.ActorId.ToString()),
                //new(ClaimTypes.UserData, user.UserId.ToString())


            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            return claims;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
               issuer: _appConfig.Issuer,
               audience: _appConfig.Issuer,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(int.Parse(_appConfig.Expires!)),
               notBefore: DateTime.UtcNow,
               signingCredentials: signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Secret!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = Encoding.UTF8.GetBytes(_appConfig.Secret!);
            var credential = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
            return credential;
        }
    }
}