using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de contratos de asesoría.
    /// </summary>
    public interface IAdvisoringContractRepositoryAsync : IGenericRepositoryAsync<AdvisoringContract, int>
    {
        Task<AdvisoringContract> GetAdvisoringContractByIdWithDetailsAsync(int id);
        Task<List<AdvisoringContract>> GetContractsByActorIdAsync(int actorId);
        Task<List<AdvisoringContract>> GetContractsByAdvisorIdAsync(int advisorId);
        Task<List<AdvisoringContract>> GetActiveContractsAsync();
        
        // Método simplificado sin paginación ni ordenamiento
        Task<List<GetAllAdvisoringContractResponse>> GetAllAdvisoringContractsAsync(string searchString = "");
        
        // Métodos adicionales que faltan
        Task<AdvisoringContract> GetByAdvisoringRequestIdAsync(int requestId);
        Task<List<AdvisoringContract>> GetAdvisoringContractsSelectAsync();
    }
}
