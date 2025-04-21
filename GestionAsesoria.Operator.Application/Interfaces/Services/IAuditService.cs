using GestionAsesoria.Operator.Application.DTOs.Audits.Response;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

      
    }
}