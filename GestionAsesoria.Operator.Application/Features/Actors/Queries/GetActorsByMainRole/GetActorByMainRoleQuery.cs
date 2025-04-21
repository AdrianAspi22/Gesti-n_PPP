using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Actor.Response;
using GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.Actors.Queries.GetSelect
{
    public class GetActorsByMainRoleQuery : IRequest<Result<IEnumerable<GetActorResearchGroupDto>>>
    {
    }

    internal class GetActorsByResearchGroupQueryHandler : IRequestHandler<GetActorsByMainRoleQuery, Result<IEnumerable<GetActorResearchGroupDto>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public GetActorsByResearchGroupQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageService = messageService;
        }

        public async Task<Result<IEnumerable<GetActorResearchGroupDto>>> Handle(GetActorsByMainRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var actors = await _unitOfWork.ActorRepository.GetResearchGroupsAsync();

                if (!actors.Any())
                {
                    string emptyMessage = _messageService.GetDynamicMessage("General", "Validation", "QueryEmpty");
                    return await Result<IEnumerable<GetActorResearchGroupDto>>.FailAsync(emptyMessage);
                }

                string successfulMessage = _messageService.GetDynamicMessage("General", "Success", "Successful");
                return await Result<IEnumerable<GetActorResearchGroupDto>>.SuccessAsync(actors, successfulMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = _messageService.GetDynamicMessage("General", "Error", "Exception");
                // Aquí podrías agregar logs con ex.Message
                return await Result<IEnumerable<GetActorResearchGroupDto>>.FailAsync(errorMessage);
            }
        }
    }
}