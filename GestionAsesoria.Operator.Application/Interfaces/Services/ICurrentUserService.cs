using GestionAsesoria.Operator.Application.Interfaces.Common;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}