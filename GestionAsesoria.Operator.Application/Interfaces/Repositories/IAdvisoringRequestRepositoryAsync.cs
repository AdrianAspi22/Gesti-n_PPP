using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    public interface IAdvisoringRequestRepositoryAsync : IGenericRepositoryAsync<AdvisoringRequest, int>
    {
        Task<AdvisoringRequest> GetAdvisoringRequestByIdWithDetailsAsync(int id);
        Task<List<AdvisoringRequest>> GetAdvisoringRequestsByActorIdAsync(int actorId);
        Task<List<AdvisoringRequest>> GetAdvisoringRequestsByAdvisorIdAsync(int advisorId);
        Task<List<AdvisoringRequest>> GetPendingAdvisoringRequestsAsync();
        
        // Método simplificado sin paginación ni ordenamiento
        Task<List<GetAllAdvisoringRequestResponse>> GetAllAdvisoringRequestsAsync(string searchString = "");
        
        // Métodos adicionales que faltan
        Task<List<AdvisoringRequest>> GetAdvisoringRequestsSelectAsync();
    }
}
