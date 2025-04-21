using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.Account;
using GestionAsesoria.Operator.Domain.Entities.Identity;
using GestionAsesoria.Operator.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Services.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AcademicUser> _userManager;
        private readonly SignInManager<AcademicUser> _signInManager;
        private readonly IUploadService _uploadService;

        public AccountService(
            UserManager<AcademicUser> userManager,
            SignInManager<AcademicUser> signInManager,
            IUploadService uploadService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uploadService = uploadService;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest model)
        {
            var user = await this._userManager.FindByIdAsync(model.ProfileId);
            if (user == null)
            {
                return await Result.FailAsync("User Not Found.");
            }

            var identityResult = await this._userManager.ChangePasswordAsync(
                user,
                model.Password!,
                model.NewPassword!);
            var errors = identityResult.Errors.Select(e => e.Description.ToString()).ToList();
            return identityResult.Succeeded ? await Result.SuccessAsync() : await Result.FailAsync(errors);
        }

        public async Task<IResult> UpdateProfileAsync(UpdateProfileRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                var userWithSamePhoneNumber = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    return await Result.FailAsync(string.Format("Phone number {0} is already used.", request.PhoneNumber));
                }
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email!);
            if (userWithSameEmail == null || userWithSameEmail.Id == request.ProfileId)
            {
                var user = await _userManager.FindByIdAsync(request.ProfileId);
                if (user == null)
                {
                    return await Result.FailAsync("User Not Found.");
                }
                user.UserName = request.UserLogin;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                //user.UserId = request.UserId;
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                if (request.PhoneNumber != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                }
                var identityResult = await _userManager.UpdateAsync(user);
                var errors = identityResult.Errors.Select(e => e.Description.ToString()).ToList();
                await _signInManager.RefreshSignInAsync(user);
                return identityResult.Succeeded ? await Result.SuccessAsync() : await Result.FailAsync(errors);
            }
            else
            {
                return await Result.FailAsync(string.Format("Email {0} is already used.", request.Email));
            }
        }
    }
}