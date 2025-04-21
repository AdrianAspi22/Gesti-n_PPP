using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Project.Request;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.Projects.Commands.Create
{
    public class CreateProjectCommand : IRequest<Result<int>>
    {
        public CreateProjectRequestDto Request { get; set; }
    }
    internal class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public CreateProjectCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // Mapear el DTO al modelo de dominio
                var projectAdd = _mapper.Map<Project>(command.Request);

                // Verificar si el Id del Actor pertenece a un rol de grupo de investigación
                var groupRoleName = "Grupo de Investigación";
                var groupActor = await _unitOfWork.Repository<Actor>()
                    .FirstOrDefaultAsync(a => a.Id == command.Request.ResearchGroupProjectId && a.MainRole.Name == groupRoleName);

                if (groupActor == null)
                {
                    return await Result<int>.FailAsync("El ID proporcionado para el Grupo no corresponde a un Actor con el rol 'Grupo de investigación'.");
                }

                // Asignar el estado inicial como "Registrado"
                var registeredState = await _unitOfWork.Repository<MasterDataValue>().FirstOrDefaultAsync(m => m.Code == "REG");
                if (registeredState == null)
                {
                    return await Result<int>.FailAsync("No se encontró el estado 'Registrado' en la base de datos.");
                }
                projectAdd.StateProjectId = registeredState.Id;


                // Agregar el proyecto a la base de datos
                await _unitOfWork.Repository<Project>().AddAsync(projectAdd);
                await _unitOfWork.Commit(cancellationToken);

                // Retornar éxito con un mensaje
                return await Result<int>.SuccessAsync(ReplyMessage.MESSAGE_SAVE);
            }
            catch (Exception)
            {
                // Retornar fallo con un mensaje genérico de excepción
                return await Result<int>.FailAsync(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
