using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.Interfaces.Common;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);
    }
}