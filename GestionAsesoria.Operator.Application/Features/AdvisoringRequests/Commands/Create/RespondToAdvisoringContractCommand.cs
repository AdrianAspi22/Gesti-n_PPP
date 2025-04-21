using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequest.Request;
using GestionAsesoria.Operator.Application.DTOs.Mail.Request;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Domain.Enums;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Commands.Create
{
    public class RespondToAdvisoringContractCommand : IRequest<Result<bool>>
    {
        public RespondToAdvisoringContractRequestDto Request { get; set; }
    }

    internal class RespondToAdvisoringContractCommandHandler : IRequestHandler<RespondToAdvisoringContractCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IMessageService _messageService;
        private readonly ILogger<RespondToAdvisoringContractCommandHandler> _logger;

        public RespondToAdvisoringContractCommandHandler(
            IMapper mapper,
            IUnitOfWork<int> unitOfWork,
            IEmailService emailService,
            IMessageService messageService,
            ILogger<RespondToAdvisoringContractCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(RespondToAdvisoringContractCommand command, CancellationToken cancellationToken)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var (advisoringRequest, advisorResearchGroup) = await ValidateRequest(command.Request);
                await UpdateAdvisoringRequest(advisoringRequest, command.Request);

                if (command.Request.ResponseStatus == AdvisoringRequestStatus.Accepted)
                {
                    await CreateAdvisoringContract(advisoringRequest);
                    await HandleStudentGroupAssignment(advisoringRequest, advisorResearchGroup);
                }

                await SendResponseEmailNotification(advisoringRequest, command.Request.ResponseStatus, command.Request);
                await _unitOfWork.Commit(cancellationToken);
                transaction.Commit();
                return await Result<bool>.SuccessAsync(true);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error al procesar la solicitud de respuesta de contrato");
                return await Result<bool>.FailAsync("Error al procesar la solicitud");
            }
        }

        private async Task UpdateAdvisoringRequest(AdvisoringRequest advisoringRequest, RespondToAdvisoringContractRequestDto request)
        {
            _mapper.Map(request, advisoringRequest);
            await _unitOfWork.AdvisoringRequestRepository.UpdateAsync(advisoringRequest);
        }

        private async Task UpdateAdvisoringContract(RespondToAdvisoringContractRequestDto request)
        {
            var advisoringContract = await _unitOfWork.AdvisoringContractRepository.GetByAdvisoringRequestIdAsync(request.AdvisoringRequestId);

            if (advisoringContract == null) return;

            advisoringContract.IsActived = request.ResponseStatus == AdvisoringRequestStatus.Accepted;
            advisoringContract.EndDate = DateTime.UtcNow.AddHours(-5);

            var contractStatusCode = request.ResponseStatus == AdvisoringRequestStatus.Accepted ? "ACCEPTED" : "REFUSED";
            var contractStatus = await _unitOfWork.MasterDataValueRepository.GetByCodeAsync(contractStatusCode);

            if (contractStatus == null)
                throw new InvalidOperationException("No se encontró un estado de contrato válido para la respuesta.");

            advisoringContract.ContractStatusId = contractStatus.Id;

            await _unitOfWork.AdvisoringContractRepository.UpdateAsync(advisoringContract);
        }

        private async Task<(AdvisoringRequest, Actor)> ValidateRequest(RespondToAdvisoringContractRequestDto request)
        {
            var advisoringRequest = await _unitOfWork.AdvisoringRequestRepository.GetByIdAsync(request.AdvisoringRequestId);
            if (advisoringRequest == null || advisoringRequest.AdvisorActorId <= 0)
                throw new InvalidOperationException("Solicitud no encontrada o ID de asesor inválido");

            var advisorResearchGroup = await GetResearchGroupByAdvisorIdAsync(advisoringRequest.AdvisorActorId);
            if (advisorResearchGroup == null)
                throw new InvalidOperationException($"No se encontró un grupo de investigación para el asesor");

            return (advisoringRequest, advisorResearchGroup);
        }

        private async Task<Actor> GetResearchGroupByAdvisorIdAsync(int advisorId)
        {
            // Verificar que el asesor existe y está activo
            var advisors = await _unitOfWork.ActorRepository.GetActorsByRoleAndStatusAsync(roleId: 15, isActive: true);
            var advisor = advisors.FirstOrDefault(a => a.Id == advisorId)
                ?? throw new InvalidOperationException($"No se encontró un docente activo con ID {advisorId}");

            // Obtener membresías activas del asesor
            var memberships = await _unitOfWork.ActorRepository.GetActiveMembershipsByAdvisorAsync(advisorId);
            var activeMembership = memberships.FirstOrDefault()
                ?? throw new InvalidOperationException($"No se encontró una membresía activa para el asesor con ID {advisorId}");

            // Obtener el grupo de investigación
            var researchGroup = await _unitOfWork.ActorRepository.GetResearchGroupByIdAsync(activeMembership.OrganizationActorId)
                ?? throw new InvalidOperationException($"No se encontró un grupo de investigación activo para el asesor con ID {advisorId}");

            return researchGroup;
        }

        private async Task CreateAdvisoringContract(AdvisoringRequest advisoringRequest)
        {
            var contractStatus = await _unitOfWork.MasterDataValueRepository.GetByCodeAsync("ACCEPTED");
            if (contractStatus == null)
                throw new InvalidOperationException("No se encontró un estado de contrato válido para la respuesta.");

            var serviceType = await _unitOfWork.MasterDataValueRepository.GetByCodeAsync("TESIS");
            if (serviceType == null)
                throw new InvalidOperationException("No se encontró un tipo de servicio válido.");

            var advisoringContract = new AdvisoringContract
            {
                ContractNumber = $"CONT-{DateTime.Now:yyyyMMdd}-{advisoringRequest.Id}",
                RegistrationDate = DateTime.Now, // Usar RegistrationDate en lugar de StartDate
                IsActived = true,
                StudentId = advisoringRequest.RequesterActorId,
                AdvisorId = advisoringRequest.AdvisorActorId,
                ContractStatusId = contractStatus.Id,
                ServiceTypeId = serviceType.Id,
                AdvisoringRequestId = advisoringRequest.Id,
                // Copiar los datos de la solicitud
                Subject = advisoringRequest.UserSubject,
                Description = advisoringRequest.UserMessage,
                // Incluir IDs de investigación
                ResearchGroupId = advisoringRequest.ResearchGroupId,
                ResearchLineId = advisoringRequest.ResearchLineId,
                ResearchAreaId = advisoringRequest.ResearchAreaId
            };

            await _unitOfWork.AdvisoringContractRepository.AddAsync(advisoringContract);

            // Asegurarse de que la solicitud tenga los IDs correctos
            await _unitOfWork.AdvisoringRequestRepository.UpdateAsync(advisoringRequest);
        }

        private async Task HandleStudentGroupAssignment(AdvisoringRequest advisoringRequest, Actor advisorResearchGroup)
        {
            // Obtener el estudiante con sus detalles
            var studentActor = await _unitOfWork.ActorRepository.GetActorByIdWithDetailsAsync(advisoringRequest.RequesterActorId)
                ?? throw new InvalidOperationException("No se encontró el estudiante.");

            // Verificar que sea un estudiante
            if (studentActor.MainRoleId != 12) // 12 = Estudiante
                throw new InvalidOperationException("El actor no es un estudiante.");

            // Verificar si ya pertenece a un grupo de investigación
            var currentResearchGroup = await _unitOfWork.ActorRepository.GetResearchGroupByIdAsync(studentActor.ParentId);

            // Si no tiene grupo o tiene uno diferente, actualizar
            if (currentResearchGroup == null || currentResearchGroup.Id != advisorResearchGroup.Id)
            {
                studentActor.ParentId = advisorResearchGroup.Id;
                await _unitOfWork.ActorRepository.UpdateAsync(studentActor);
            }
        }

        private async Task SendResponseEmailNotification(AdvisoringRequest advisoringRequest, AdvisoringRequestStatus responseStatus, RespondToAdvisoringContractRequestDto request)
        {
            var studentActor = await _unitOfWork.ActorRepository.GetByIdAsync(advisoringRequest.RequesterActorId);
            var advisorActor = await _unitOfWork.ActorRepository.GetByIdAsync(advisoringRequest.AdvisorActorId);

            string templateKey = responseStatus == AdvisoringRequestStatus.Accepted ? "AcceptanceNotification" : "RefusalNotification";
            string defaultTemplate = responseStatus == AdvisoringRequestStatus.Accepted
                ? "Estimado {0},\n\nSu Solicitud de Asesoría Académica ha sido ACEPTADA por el Docente {1}.\n\nRespuesta del Asesor: {2}.\n\nSaludos cordiales."
                : "Estimado {0},\n\nSu Solicitud de Asesoría Académica ha sido RECHAZADA por el Docente {1}.\n\nRespuesta del Asesor: {2}.\n\nSaludos cordiales.";

            string emailBody = string.Format(
                _messageService.GetMessage("AdvisoringContract", "EmailTemplates", templateKey) ?? defaultTemplate,
                $"{studentActor?.FirstName ?? "Estudiante"} {studentActor?.SecondName}",
                advisorActor?.FirstName ?? "Docente",
                request.ResponseAdvisor
            );

            await _emailService.SendEmailAsync(new MailRequest
            {
                To = studentActor.Email,
                Subject = "Respuesta a Solicitud de Asesoría",
                Body = emailBody,
                From = _messageService.GetMessage("General", "EmailSettings", "DefaultSenderEmail"),
                IsBodyHtml = false
            });
        }

        private async Task<int> CreateContractAsync(AdvisoringRequest request, int activeStatusId, Actor researchGroup, int researchLineId, int researchAreaId)
        {
            try
            {
                // Crear el contrato
                var contract = new AdvisoringContract
                {
                    ContractNumber = $"CONT-{DateTime.Now:yyyyMMdd}-{request.Id}",
                    RegistrationDate = DateTime.Now, // Usar RegistrationDate en lugar de StartDate
                    IsActived = true,
                    StudentId = request.RequesterActorId,
                    AdvisorId = request.AdvisorActorId,
                    ContractStatusId = activeStatusId,
                    ServiceTypeId = request.ServiceTypeId,
                    AdvisoringRequestId = request.Id,
                    // Copiar los datos de la solicitud
                    Subject = request.UserSubject,
                    Description = request.UserMessage,
                    // Usar los IDs de los actores para los grupos de investigación
                    ResearchGroupId = researchGroup.Id,
                    ResearchLineId = researchLineId,
                    ResearchAreaId = researchAreaId
                };

                // Guardar el contrato
                var addedContract = await _unitOfWork.AdvisoringContractRepository.AddAsync(contract);

                // Actualizar la solicitud con los IDs de los grupos de investigación
                request.ResearchGroupId = researchGroup.Id;
                request.ResearchLineId = researchLineId;
                request.ResearchAreaId = researchAreaId;

                await _unitOfWork.AdvisoringRequestRepository.UpdateAsync(request);

                return addedContract.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el contrato de asesoría");
                throw;
            }
        }

    }
}