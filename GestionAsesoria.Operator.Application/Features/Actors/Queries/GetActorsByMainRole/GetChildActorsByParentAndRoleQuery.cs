using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Actor.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.Actors.Queries.GetActorsByMainRole
{
    public class GetChildActorsByParentAndRoleQuery : IRequest<Result<IEnumerable<ActorResponseDto>>>
    {
        public int ParentId { get; set; } // ID del grupo de investigación seleccionado
        public int RoleId { get; set; } // ID del tipo de actor a buscar (línea, área, etc.)
    }
    internal class GetChildActorsByParentAndRoleQueryHandler : IRequestHandler<GetChildActorsByParentAndRoleQuery, Result<IEnumerable<ActorResponseDto>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public GetChildActorsByParentAndRoleQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IMessageService messageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageService = messageService;
        }

        public async Task<Result<IEnumerable<ActorResponseDto>>> Handle(GetChildActorsByParentAndRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener actores relacionados al grupo y rol específico
                var actors = await _unitOfWork.ActorRepository.GetChildActorsByParentAndRoleAsync(request.ParentId, request.RoleId);

                if (!actors.Any())
                {
                    string emptyMessage = _messageService.GetDynamicMessage("General", "Validation", "QueryEmpty");
                    return await Result<IEnumerable<ActorResponseDto>>.FailAsync(emptyMessage); // Respuesta para datos vacíos
                }

                // Mapear actores a DTOs para la respuesta
                var actorDtos = _mapper.Map<IEnumerable<ActorResponseDto>>(actors);

                string successfulMessage = _messageService.GetDynamicMessage("General", "Success", "Successful");
                return await Result<IEnumerable<ActorResponseDto>>.SuccessAsync(actorDtos, successfulMessage); // Respuesta exitosa
            }
            catch (Exception ex)
            {
                string errorMessage = _messageService.GetDynamicMessage("General", "Error", "Exception");
                // Aquí podrías agregar logs con ex.Message para monitorear errores
                return await Result<IEnumerable<ActorResponseDto>>.FailAsync(errorMessage); // Respuesta para errores
            }
        }
    }
}
