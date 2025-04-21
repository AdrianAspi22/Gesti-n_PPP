using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using GestionAsesoria.Operator.Application.Interfaces.Common;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services.Identity
{
    public interface IUserService : IService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();

        //Task<Result<List<UserResponse>>> GetAllByChargeIdAsync(int chargeId);
        Task<Result<List<UserSelectResponse>>> GetUserSelectAsync();
        Task<int> GetCountAsync();

        Task<IResult<UserByIdResponse>> GetAsync(string userId);

        Task<IResult> RegisterAsync(RegisterRequest request);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        Task<IResult<UserRolesResponse>> GetRolesAsync(string id);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

    }
}