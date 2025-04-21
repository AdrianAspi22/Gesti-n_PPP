using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio para contratos de asesoría.
    /// </summary>
    public class AdvisoringContractsRepositoryAsync : GenericRepositoryAsync<AdvisoringContract, int>, IAdvisoringContractRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<AdvisoringContract> _advisoringContracts;
        private readonly DbSet<Actor> _actors;

        public AdvisoringContractsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _advisoringContracts = _context.Set<AdvisoringContract>();
            _actors = _context.Set<Actor>();
        }

        public async Task<AdvisoringContract> GetAdvisoringContractByIdWithDetailsAsync(int id)
        {
            return await _advisoringContracts
                .Include(ac => ac.StudentActor)
                .Include(ac => ac.AdvisorActor)
                .Include(ac => ac.ServiceType)
                .FirstOrDefaultAsync(ac => ac.Id == id);
        }

        public async Task<List<AdvisoringContract>> GetContractsByActorIdAsync(int actorId)
        {
            return await _advisoringContracts
                .Where(ac => ac.StudentId == actorId || ac.AdvisorId == actorId)
                .ToListAsync();
        }

        public async Task<List<AdvisoringContract>> GetContractsByAdvisorIdAsync(int advisorId)
        {
            return await _advisoringContracts
                .Where(ac => ac.AdvisorId == advisorId)
                .ToListAsync();
        }

        public async Task<List<AdvisoringContract>> GetActiveContractsAsync()
        {
            return await _advisoringContracts
                .Where(ac => ac.IsActived)
             .ToListAsync();
        }

        public async Task<AdvisoringContract> GetByAdvisoringRequestIdAsync(int requestId)
        {
            return await _advisoringContracts
                .Include(ac => ac.StudentActor)
                .Include(ac => ac.AdvisorActor)
                .Include(ac => ac.ServiceType)
                .FirstOrDefaultAsync(ac => ac.AdvisoringRequestId == requestId);
        }

        public async Task<List<AdvisoringContract>> GetAdvisoringContractsSelectAsync()
        {
            return await _advisoringContracts
                .Include(ac => ac.StudentActor)
                .Include(ac => ac.AdvisorActor)
                .Where(ac => ac.IsActived)
                .ToListAsync();
        }

        public async Task<List<GetAllAdvisoringContractResponse>> GetAllAdvisoringContractsAsync(string searchString = "")
        {
            var query = _advisoringContracts
                .Include(ac => ac.StudentActor)
                .Include(ac => ac.AdvisorActor)
                .Include(ac => ac.ServiceType)
                .AsNoTracking()
                .Select(ac => new GetAllAdvisoringContractResponse
                {
                    Id = ac.Id,
                    ContractNumber = ac.ContractNumber,
                    StartDate = ac.RegistrationDate,
                    EndDate = ac.EndDate,
                    IsActived = ac.IsActived,
                    Status = ac.IsActived ? "Activo" : "Inactivo",

                    StudentId = ac.StudentId,
                    StudentName = ac.StudentActor.FirstName,
                    StudentLastName = ac.StudentActor.SecondName,
                    StudentEmail = ac.StudentActor.Email,

                    AdvisorId = ac.AdvisorId,
                    AdvisorName = ac.AdvisorActor.FirstName,
                    AdvisorLastName = ac.AdvisorActor.SecondName,
                    AdvisorEmail = ac.AdvisorActor.Email,

                    // Estos campos se llenarán con información de los actores relacionados
                    ResearchGroupId = ac.ResearchGroupId ?? 0,
                    ResearchGroupName = ac.ResearchGroupId.HasValue ?
                        _actors.FirstOrDefault(a => a.Id == ac.ResearchGroupId).FirstName : "",
                    ResearchGroupAcronym = ac.ResearchGroupId.HasValue ?
                        _actors.FirstOrDefault(a => a.Id == ac.ResearchGroupId).SecondName : "",

                    ResearchLineId = ac.ResearchLineId ?? 0,
                    ResearchLineName = ac.ResearchLineId.HasValue ?
                        _actors.FirstOrDefault(a => a.Id == ac.ResearchLineId).FirstName : "",

                    ResearchAreaId = ac.ResearchAreaId ?? 0,
                    ResearchAreaName = ac.ResearchAreaId.HasValue ?
                        _actors.FirstOrDefault(a => a.Id == ac.ResearchAreaId).FirstName : "",

                    ServiceTypeId = ac.ServiceTypeId,
                    ServiceTypeName = ac.ServiceType.Name
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(ac =>
                    ac.ContractNumber.ToLower().Contains(searchString) ||
                    ac.StudentName.ToLower().Contains(searchString) ||
                    ac.StudentLastName.ToLower().Contains(searchString) ||
                    ac.AdvisorName.ToLower().Contains(searchString) ||
                    ac.AdvisorLastName.ToLower().Contains(searchString) ||
                    ac.ResearchGroupName.ToLower().Contains(searchString)
                );
            }

            // Ordenamiento por defecto
            query = query.OrderByDescending(ac => ac.StartDate);

            return await query.ToListAsync();
        }
    }
}
