using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetAllAdvisoringRequests;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Wrappers;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Domain.Enums;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Extensions;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Repositories
{
    public class AdvisoringRequestRepositoryAsync : GenericRepositoryAsync<AdvisoringRequest, int>, IAdvisoringRequestRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<AdvisoringRequest> _advisoringRequests;

        public AdvisoringRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _advisoringRequests = _context.Set<AdvisoringRequest>();
        }

        public async Task<AdvisoringRequest> SaveAdvisoringRequest(AdvisoringRequest advisoringRequest)
        {
            await _advisoringRequests.AddAsync(advisoringRequest);
            return advisoringRequest;
        }

        public async Task<int?> GetStudentRequesterIdAsync(int advisoringRequestId)
        {
            return await _advisoringRequests
                .Where(ar => ar.Id == advisoringRequestId)
                .Join(_context.Actor,
                    request => request.RequesterActorId,
                    actor => actor.Id,
            (request, actor) => actor)
                .Join(_context.MasterDataValue,
                    actor => actor.ActorTypeId,
                    actorType => actorType.Id,
                    (actor, actorType) => new { Actor = actor, ActorType = actorType })
                .Where(x => x.ActorType.Name == "Estudiante")
                .Select(x => x.Actor.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> GetAdvisorRequesterIdAsync(int advisoringRequestId)
        {
            return await _advisoringRequests
                .Where(ar => ar.Id == advisoringRequestId)
                .Join(_context.Actor,
                    request => request.RequesterActorId,
                    actor => actor.Id,
            (request, actor) => actor)
                .Join(_context.MasterDataValue,
                    actor => actor.ActorTypeId,
                    actorType => actorType.Id,
                    (actor, actorType) => new { Actor = actor, ActorType = actorType })
                .Where(x => x.ActorType.Name == "Docente")
                .Select(x => x.Actor.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<AdvisoringRequestResponseDto>> GetAdvisoringRequests(GetAllAdvisoringRequestParameters parameters)
        {
            var query = from ra in _advisoringRequests
                        select new AdvisoringRequestResponseDto
                        {
                            Id = ra.Id,
                            DateRequest = ra.DateRequest,
                            DateResponseAdvisor = ra.DateResponseAdvisor,
                            UserSubject = ra.UserSubject,
                            UserMessage = ra.UserMessage,
                            ResponseAdvisor = ra.ResponseAdvisor,
                            ServiceTypeId = ra.ServiceTypeId,
                            RequesterActorId = ra.RequesterActorId,
                            AdvisorActorId = ra.AdvisorActorId
                        };

            if (!string.IsNullOrEmpty(parameters.SearchString))
            {
                query = query.Where(a => a.ResponseAdvisor.Contains(parameters.SearchString));
            }

            if (parameters.advisoringRequestIds.Length > 0)
            {
                query = query.Where(ra => parameters.advisoringRequestIds.Contains(ra.Id));
            }

            var orderedQuery = query.OrderByDescending(ra => ra.Id);
            return await orderedQuery.AsNoTracking().ToPaginatedListAsync<AdvisoringRequestResponseDto>(parameters.pageNumber, parameters.pageSize);
        }

        public async Task<AdvisoringRequest> GetAdvisoringRequestByIdWithDetailsAsync(int id)
        {
            return await _advisoringRequests
                .Include(ar => ar.RequesterActor)
                .Include(ar => ar.AdvisorActor)
                .Include(ar => ar.ServiceType)
                .FirstOrDefaultAsync(ar => ar.Id == id);
        }

        public async Task<List<AdvisoringRequest>> GetAdvisoringRequestsByActorIdAsync(int actorId)
        {
            return await _advisoringRequests
                .Where(ar => ar.RequesterActorId == actorId)
                .ToListAsync();
        }

        public async Task<List<AdvisoringRequest>> GetAdvisoringRequestsByAdvisorIdAsync(int advisorId)
        {
            return await _advisoringRequests
                .Where(ar => ar.AdvisorActorId == advisorId)
                .ToListAsync();
        }

        public async Task<List<AdvisoringRequest>> GetPendingAdvisoringRequestsAsync()
        {
            return await _advisoringRequests
                .Where(ar => ar.AdvisoringRequestStatus == AdvisoringRequestStatus.PendingRequest)
                .ToListAsync();
        }

        public async Task<List<AdvisoringRequest>> GetAdvisoringRequestsSelectAsync()
        {
            return await _advisoringRequests
                .Include(ar => ar.RequesterActor)
                .Include(ar => ar.AdvisorActor)
                .Include(ar => ar.ServiceType)
                .ToListAsync();
        }

        public async Task<List<GetAllAdvisoringRequestResponse>> GetAllAdvisoringRequestsAsync(string searchString = "")
        {
            var query = _advisoringRequests
                .Include(ar => ar.RequesterActor)
                .Include(ar => ar.AdvisorActor)
                .Include(ar => ar.ServiceType)
                .AsNoTracking()
                .Select(ar => new GetAllAdvisoringRequestResponse
                {
                    Id = ar.Id,
                    UserSubject = ar.UserSubject,
                    UserMessage = ar.UserMessage,
                    DateRequest = ar.DateRequest,
                    AdvisoringRequestStatus = (int)ar.AdvisoringRequestStatus,
                    StatusName = ar.AdvisoringRequestStatus == AdvisoringRequestStatus.PendingRequest ? "Pendiente" :
                                ar.AdvisoringRequestStatus == AdvisoringRequestStatus.Accepted ? "Aprobado" :
                                ar.AdvisoringRequestStatus == AdvisoringRequestStatus.Refused ? "Rechazado" : "Desconocido",

                    RequesterActorId = ar.RequesterActorId,
                    RequesterName = ar.RequesterActor.FirstName,
                    RequesterLastName = ar.RequesterActor.SecondName,
                    RequesterEmail = ar.RequesterActor.Email,

                    AdvisorActorId = ar.AdvisorActorId,
                    AdvisorName = ar.AdvisorActor.FirstName,
                    AdvisorLastName = ar.AdvisorActor.SecondName,
                    AdvisorEmail = ar.AdvisorActor.Email,

                    ServiceTypeId = ar.ServiceTypeId,
                    ServiceTypeName = ar.ServiceType.Name
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(ar =>
                    ar.UserSubject.ToLower().Contains(searchString) ||
                    ar.RequesterName.ToLower().Contains(searchString) ||
                    ar.RequesterLastName.ToLower().Contains(searchString) ||
                    ar.AdvisorName.ToLower().Contains(searchString) ||
                    ar.AdvisorLastName.ToLower().Contains(searchString)
                );
            }

            // Ordenamiento por defecto
            query = query.OrderByDescending(ar => ar.DateRequest);

            return await query.ToListAsync();
        }
    }
}
