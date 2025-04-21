using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Request;
using GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Commands.Create;
using GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Commands.Create;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GestionAsesoria.Operator.Tests.Features.AdvisoringContracts
{
    public class AdvisoringContractHandlersTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork<int>> _unitOfWorkMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<ILogger<RespondToAdvisoringContractCommandHandler>> _loggerMock;
        private readonly Mock<IActorRepositoryAsync> _actorRepositoryMock;
        private readonly Mock<IAdvisoringContractRepositoryAsync> _advisoringContractRepositoryMock;
        private readonly Mock<IAdvisoringRequestRepositoryAsync> _advisoringRequestRepositoryMock;
        private readonly Mock<IMasterDataValueRepositoryAsync> _masterDataValueRepositoryMock;

        public AdvisoringContractHandlersTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork<int>>();
            _emailServiceMock = new Mock<IEmailService>();
            _messageServiceMock = new Mock<IMessageService>();
            _loggerMock = new Mock<ILogger<RespondToAdvisoringContractCommandHandler>>();

            _actorRepositoryMock = new Mock<IActorRepositoryAsync>();
            _advisoringContractRepositoryMock = new Mock<IAdvisoringContractRepositoryAsync>();
            _advisoringRequestRepositoryMock = new Mock<IAdvisoringRequestRepositoryAsync>();
            _masterDataValueRepositoryMock = new Mock<IMasterDataValueRepositoryAsync>();

            _unitOfWorkMock.Setup(u => u.ActorRepository).Returns(_actorRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.AdvisoringContractRepository).Returns(_advisoringContractRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.AdvisoringRequestRepository).Returns(_advisoringRequestRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.MasterDataValueRepository).Returns(_masterDataValueRepositoryMock.Object);

            // Setup transaction mock
            var transactionMock = new Mock<IUnitOfWorkTransaction>();
            _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(transactionMock.Object);
        }

        #region CreateAdvisoringContract Tests

        [Fact]
        public async Task Handle_CreateAdvisoringContract_Success()
        {
            // Arrange
            var command = new CreateAdvisoringContractCommand
            {
                Request = new CreateAdvisoringContractRequestDto
                {
                    DocenteId = 1,
                    EstudianteId = 2,
                    Subject = "Test Subject",
                    Description = "Test Description",
                    ResearchGroupId = 1,
                    ResearchLineId = 1,
                    ResearchAreaId = 1
                }
            };

            // Setup actor repository mocks
            var advisor = new Actor { Id = 1, MainRoleId = 15, FirstName = "Advisor", Email = "advisor@test.com" };
            var student = new Actor { Id = 2, MainRoleId = 12, FirstName = "Student", Email = "student@test.com" };
            var researchGroup = new Actor { Id = 1, MainRoleId = 20 };

            _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(1)).ReturnsAsync(advisor);
            _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(2)).ReturnsAsync(student);
            _actorRepositoryMock.Setup(a => a.GetResearchGroupWithDetailsAsync(1, 1, 1, 1)).ReturnsAsync(researchGroup);

            // Setup master data repository mocks
            var serviceType = new MasterDataValue { Id = 1, Code = "TESIS" };
            _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("TESIS")).ReturnsAsync(serviceType);

            // Setup advisoring contract repository mocks
            _advisoringRequestRepositoryMock.Setup(r => r.AddAsync(It.IsAny<AdvisoringRequest>()))
                .ReturnsAsync((AdvisoringRequest request) => { request.Id = 1; return request; });

            // Setup existing contracts check
            _advisoringContractRepositoryMock.Setup(c => c.GetContractsByActorIdAsync(2))
                .ReturnsAsync(new List<AdvisoringContract>());

            // Setup message service
            _messageServiceMock.Setup(m => m.GetDynamicMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Test message");
            _messageServiceMock.Setup(m => m.GetMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Test email template");

            var handler = new CreateAdvisoringContractCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _emailServiceMock.Object,
                _messageServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(1, result.Data);
            _advisoringRequestRepositoryMock.Verify(r => r.AddAsync(It.IsAny<AdvisoringRequest>()), Times.Once);
            _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_CreateAdvisoringContract_WithExistingActiveContract_ShouldFail()
        {
            // Arrange
            var command = new CreateAdvisoringContractCommand
            {
                Request = new CreateAdvisoringContractRequestDto
                {
                    DocenteId = 1,
                    EstudianteId = 2,
                    Subject = "Test Subject",
                    Description = "Test Description",
                    ResearchGroupId = 1,
                    ResearchLineId = 1,
                    ResearchAreaId = 1
                }
            };

            // Setup existing active contract
            _advisoringContractRepositoryMock.Setup(c => c.GetContractsByActorIdAsync(2))
                .ReturnsAsync(new List<AdvisoringContract>
                {
                    new AdvisoringContract { Id = 1, IsActived = true }
                });

            // Setup message service
            _messageServiceMock.Setup(m => m.GetDynamicMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Active contract exists");

            var handler = new CreateAdvisoringContractCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _emailServiceMock.Object,
                _messageServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Active contract exists", result.Messages[0]);
        }

        [Fact]
        public async Task Handle_CreateAdvisoringContract_InvalidStudent_ShouldFail()
        {
            // Arrange
            var command = new CreateAdvisoringContractCommand
            {
                Request = new CreateAdvisoringContractRequestDto
                {
                    DocenteId = 1,
                    EstudianteId = 2,
                    Subject = "Test Subject",
                    Description = "Test Description",
                    ResearchGroupId = 1,
                    ResearchLineId = 1,
                    ResearchAreaId = 1
                }
            };

            // Setup no active contracts
            _advisoringContractRepositoryMock.Setup(c => c.GetContractsByActorIdAsync(2))
                .ReturnsAsync(new List<AdvisoringContract>());

            // Setup invalid student (wrong role)
            var invalidStudent = new Actor { Id = 2, MainRoleId = 15 }; // Not a student role
            _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(2)).ReturnsAsync(invalidStudent);

            // Setup message service
            _messageServiceMock.Setup(m => m.GetDynamicMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Test error message");

            var handler = new CreateAdvisoringContractCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _emailServiceMock.Object,
                _messageServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("no es un estudiante válido", result.Messages[0]);
        }

        [Fact]
        public async Task Handle_CreateAdvisoringContract_InvalidResearchGroup_ShouldFail()
        {
            // Arrange
            var command = new CreateAdvisoringContractCommand
            {
                Request = new CreateAdvisoringContractRequestDto
                {
                    DocenteId = 1,
                    EstudianteId = 2,
                    Subject = "Test Subject",
                    Description = "Test Description",
                    ResearchGroupId = 1,
                    ResearchLineId = 1,
                    ResearchAreaId = 1
                }
            };

            // Setup no active contracts
            _advisoringContractRepositoryMock.Setup(c => c.GetContractsByActorIdAsync(2))
                .ReturnsAsync(new List<AdvisoringContract>());

            // Setup research group not found
            _actorRepositoryMock.Setup(a => a.GetResearchGroupWithDetailsAsync(1, 1, 1, 1))
                .ReturnsAsync((Actor)null);

            // Setup message service
            _messageServiceMock.Setup(m => m.GetDynamicMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Filter_Tacher");

            var handler = new CreateAdvisoringContractCommandHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _emailServiceMock.Object,
                _messageServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Filter_Tacher", result.Messages[0]);
        }

        #endregion

        //#region RespondToAdvisoringContract Tests

        //[Fact]
        //public async Task Handle_RespondToAdvisoringContract_AcceptedStatus_Success()
        //{
        //    // Arrange
        //    var command = new RespondToAdvisoringContractCommand
        //    {
        //        Request = new RespondToAdvisoringContractRequestDto
        //        {
        //            AdvisoringRequestId = 1,
        //            ResponseStatus = AdvisoringRequestStatus.Accepted,
        //            ResponseAdvisor = "Accepted test response"
        //        }
        //    };

        //    // Setup advisoring request
        //    var advisoringRequest = new AdvisoringRequest
        //    {
        //        Id = 1,
        //        AdvisorActorId = 1,
        //        RequesterActorId = 2,
        //        UserSubject = "Test Subject",
        //        UserMessage = "Test Description",
        //        ServiceTypeId = 1,
        //        ResearchGroupId = 1,
        //        ResearchLineId = 1,
        //        ResearchAreaId = 1,
        //        AdvisoringRequestStatus = AdvisoringRequestStatus.PendingRequest // Status inicial
        //    };
        //    _advisoringRequestRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(advisoringRequest);

        //    // Setup advisor and research group
        //    var advisors = new List<Actor> { new Actor { Id = 1, MainRoleId = 15 } };
        //    _actorRepositoryMock.Setup(a => a.GetActorsByRoleAndStatusAsync(15, true)).ReturnsAsync(advisors);

        //    var memberships = new List<Membership>
        //{
        //    new Membership { OrganizationActorId = 1 }
        //};
        //    _actorRepositoryMock.Setup(a => a.GetActiveMembershipsByAdvisorAsync(1)).ReturnsAsync(memberships);

        //    var researchGroup = new Actor { Id = 1, MainRoleId = 20 };
        //    _actorRepositoryMock.Setup(a => a.GetResearchGroupByIdAsync(1)).ReturnsAsync(researchGroup);

        //    // Setup student
        //    var student = new Actor
        //    {
        //        Id = 2,
        //        MainRoleId = 12,
        //        FirstName = "Student",
        //        Email = "student@test.com",
        //        ParentId = null // No research group yet
        //    };
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(2)).ReturnsAsync(student);
        //    _actorRepositoryMock.Setup(a => a.GetActorByIdWithDetailsAsync(2)).ReturnsAsync(student);
        //    _actorRepositoryMock.Setup(a => a.UpdateAsync(It.IsAny<Actor>()))
        //        .Returns(Task.FromResult(student)); // Use Returns with Task.FromResult instead of ReturnsAsync

        //    // Setup advisor actor
        //    var advisor = new Actor { Id = 1, FirstName = "Advisor", Email = "advisor@test.com" };
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync(advisor);
        //    _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(1)).ReturnsAsync(advisor);

        //    // Setup master data values
        //    var acceptedStatus = new MasterDataValue { Id = 2, Code = "ACCEPTED" };
        //    var thesisType = new MasterDataValue { Id = 1, Code = "TESIS" };
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("ACCEPTED")).ReturnsAsync(acceptedStatus);
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByIdAsync(1)).ReturnsAsync(thesisType);
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("TESIS")).ReturnsAsync(thesisType);

        //    // Setup mapper mock con Callback para simular el comportamiento real
        //    _mapperMock.Setup(m => m.Map(It.IsAny<RespondToAdvisoringContractRequestDto>(), It.IsAny<AdvisoringRequest>()))
        //        .Callback<object, object>((src, dest) =>
        //        {
        //            var request = src as RespondToAdvisoringContractRequestDto;
        //            var advRequest = dest as AdvisoringRequest;
        //            if (request != null && advRequest != null)
        //            {
        //                advRequest.AdvisoringRequestStatus = request.ResponseStatus;
        //                advRequest.ResponseAdvisor = request.ResponseAdvisor;
        //            }
        //        });

        //    // Setup contract repository
        //    _advisoringContractRepositoryMock.Setup(c => c.AddAsync(It.IsAny<AdvisoringContract>()))
        //        .ReturnsAsync((AdvisoringContract contract) =>
        //        {
        //            contract.Id = 1;
        //            return contract;
        //        });

        //    // Setup message service
        //    _messageServiceMock.Setup(m => m.GetMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns("Test email template");
        //    _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()))
        //        .Returns(Task.FromResult(true)); // Use Returns with Task.FromResult instead of ReturnsAsync

        //    var handler = new RespondToAdvisoringContractCommandHandler(
        //        _mapperMock.Object,
        //        _unitOfWorkMock.Object,
        //        _emailServiceMock.Object,
        //        _messageServiceMock.Object,
        //        _loggerMock.Object);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.Succeeded);
        //    Assert.True(result.Data);

        //    _advisoringRequestRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<AdvisoringRequest>()), Times.AtLeastOnce);
        //    _advisoringContractRepositoryMock.Verify(c => c.AddAsync(It.IsAny<AdvisoringContract>()), Times.Once);
        //    _actorRepositoryMock.Verify(a => a.UpdateAsync(It.Is<Actor>(s => s.Id == 2)), Times.Once);
        //    _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()), Times.Once);
        //    _unitOfWorkMock.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task Handle_RespondToAdvisoringContract_RejectedStatus_Success()
        //{
        //    // Arrange
        //    var command = new RespondToAdvisoringContractCommand
        //    {
        //        Request = new RespondToAdvisoringContractRequestDto
        //        {
        //            AdvisoringRequestId = 1,
        //            ResponseStatus = AdvisoringRequestStatus.Refused,
        //            ResponseAdvisor = "Rejected test response"
        //        }
        //    };

        //    // Setup advisoring request
        //    var advisoringRequest = new AdvisoringRequest
        //    {
        //        Id = 1,
        //        AdvisorActorId = 1,
        //        RequesterActorId = 2,
        //        UserSubject = "Test Subject",
        //        UserMessage = "Test Description",
        //        AdvisoringRequestStatus = AdvisoringRequestStatus.PendingRequest // Status inicial
        //    };
        //    _advisoringRequestRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(advisoringRequest);

        //    // Setup student and advisor actors
        //    var student = new Actor { Id = 2, FirstName = "Student", Email = "student@test.com" };
        //    var advisor = new Actor { Id = 1, FirstName = "Advisor", Email = "advisor@test.com" };
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(2)).ReturnsAsync(student);
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync(advisor);
        //    _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(1)).ReturnsAsync(advisor);

        //    // Setup master data values
        //    var refusedStatus = new MasterDataValue { Id = 3, Code = "REFUSED" };
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("REFUSED")).ReturnsAsync(refusedStatus);

        //    // Setup mapper mock con Callback para simular el comportamiento real
        //    _mapperMock.Setup(m => m.Map(It.IsAny<RespondToAdvisoringContractRequestDto>(), It.IsAny<AdvisoringRequest>()))
        //        .Callback<object, object>((src, dest) =>
        //        {
        //            var request = src as RespondToAdvisoringContractRequestDto;
        //            var advRequest = dest as AdvisoringRequest;
        //            if (request != null && advRequest != null)
        //            {
        //                advRequest.AdvisoringRequestStatus = request.ResponseStatus;
        //                advRequest.ResponseAdvisor = request.ResponseAdvisor;
        //            }
        //        });

        //    // Setup email service
        //    _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()))
        //        .Returns(Task.FromResult(true)); // Use Returns with Task.FromResult instead of ReturnsAsync

        //    // Setup message service
        //    _messageServiceMock.Setup(m => m.GetMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns("Test email template");

        //    var handler = new RespondToAdvisoringContractCommandHandler(
        //        _mapperMock.Object,
        //        _unitOfWorkMock.Object,
        //        _emailServiceMock.Object,
        //        _messageServiceMock.Object,
        //        _loggerMock.Object);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.Succeeded);
        //    Assert.True(result.Data);

        //    _advisoringRequestRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<AdvisoringRequest>()), Times.Once);
        //    _advisoringContractRepositoryMock.Verify(c => c.AddAsync(It.IsAny<AdvisoringContract>()), Times.Never);
        //    _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()), Times.Once);
        //    _unitOfWorkMock.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task Handle_RespondToAdvisoringContract_AdvisoringRequestNotFound_ShouldFail()
        //{
        //    // Arrange
        //    var command = new RespondToAdvisoringContractCommand
        //    {
        //        Request = new RespondToAdvisoringContractRequestDto
        //        {
        //            AdvisoringRequestId = 1,
        //            ResponseStatus = AdvisoringRequestStatus.Accepted,
        //            ResponseAdvisor = "Test response"
        //        }
        //    };

        //    // Setup advisoring request not found
        //    _advisoringRequestRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((AdvisoringRequest)null);

        //    // Setup transaction mock para asegurar que se maneje correctamente
        //    var transactionMock = new Mock<IUnitOfWorkTransaction>();
        //    _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(transactionMock.Object);

        //    // Setup message service para el mensaje de error
        //    _messageServiceMock.Setup(m => m.GetDynamicMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns("Error al procesar la solicitud");

        //    var handler = new RespondToAdvisoringContractCommandHandler(
        //        _mapperMock.Object,
        //        _unitOfWorkMock.Object,
        //        _emailServiceMock.Object,
        //        _messageServiceMock.Object,
        //        _loggerMock.Object);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.False(result.Succeeded);
        //    Assert.Contains("Error al procesar la solicitud", result.Messages[0]);
        //    _loggerMock.Verify(l => l.LogError(
        //        It.IsAny<Exception>(),
        //        It.Is<string>(s => s.Contains("Error al procesar la solicitud"))),
        //        Times.Once);
        //}

        //[Fact]
        //public async Task Handle_RespondToAdvisoringContract_StudentNotInResearchGroup_ShouldUpdateStudentGroup()
        //{
        //    // Arrange
        //    var command = new RespondToAdvisoringContractCommand
        //    {
        //        Request = new RespondToAdvisoringContractRequestDto
        //        {
        //            AdvisoringRequestId = 1,
        //            ResponseStatus = AdvisoringRequestStatus.Accepted,
        //            ResponseAdvisor = "Accepted test response"
        //        }
        //    };

        //    // Setup advisoring request
        //    var advisoringRequest = new AdvisoringRequest
        //    {
        //        Id = 1,
        //        AdvisorActorId = 1,
        //        RequesterActorId = 2,
        //        UserSubject = "Test Subject",
        //        UserMessage = "Test Description",
        //        ServiceTypeId = 1,
        //        ResearchGroupId = 1,
        //        ResearchLineId = 1,
        //        ResearchAreaId = 1,
        //        AdvisoringRequestStatus = AdvisoringRequestStatus.PendingRequest // Status inicial
        //    };
        //    _advisoringRequestRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(advisoringRequest);

        //    // Setup advisor and research group
        //    var advisors = new List<Actor> { new Actor { Id = 1, MainRoleId = 15 } };
        //    _actorRepositoryMock.Setup(a => a.GetActorsByRoleAndStatusAsync(15, true)).ReturnsAsync(advisors);

        //    var memberships = new List<Membership>
        //{
        //    new Membership { OrganizationActorId = 1 }
        //};
        //    _actorRepositoryMock.Setup(a => a.GetActiveMembershipsByAdvisorAsync(1)).ReturnsAsync(memberships);

        //    var researchGroup = new Actor { Id = 1, MainRoleId = 20 };
        //    _actorRepositoryMock.Setup(a => a.GetResearchGroupByIdAsync(1)).ReturnsAsync(researchGroup);

        //    // Setup student with different research group
        //    var student = new Actor
        //    {
        //        Id = 2,
        //        MainRoleId = 12,
        //        FirstName = "Student",
        //        Email = "student@test.com",
        //        ParentId = 2 // Different research group
        //    };
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(2)).ReturnsAsync(student);
        //    _actorRepositoryMock.Setup(a => a.GetActorByIdWithDetailsAsync(2)).ReturnsAsync(student);
        //    _actorRepositoryMock.Setup(a => a.UpdateAsync(It.IsAny<Actor>()))
        //        .Returns(Task.FromResult(student)); // Use Returns with Task.FromResult instead of ReturnsAsync

        //    // Setup student's current research group
        //    var differentResearchGroup = new Actor { Id = 2, MainRoleId = 20 };
        //    _actorRepositoryMock.Setup(a => a.GetResearchGroupByIdAsync(2)).ReturnsAsync(differentResearchGroup);

        //    // Setup advisor actor
        //    var advisor = new Actor { Id = 1, FirstName = "Advisor", Email = "advisor@test.com" };
        //    _actorRepositoryMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync(advisor);
        //    _actorRepositoryMock.Setup(a => a.GetActorWithDetailsAsync(1)).ReturnsAsync(advisor);

        //    // Setup master data values
        //    var acceptedStatus = new MasterDataValue { Id = 2, Code = "ACCEPTED" };
        //    var thesisType = new MasterDataValue { Id = 1, Code = "TESIS" };
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("ACCEPTED")).ReturnsAsync(acceptedStatus);
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByIdAsync(1)).ReturnsAsync(thesisType);
        //    _masterDataValueRepositoryMock.Setup(m => m.GetByCodeAsync("TESIS")).ReturnsAsync(thesisType);

        //    // Setup mapper mock con Callback
        //    _mapperMock.Setup(m => m.Map(It.IsAny<RespondToAdvisoringContractRequestDto>(), It.IsAny<AdvisoringRequest>()))
        //        .Callback<object, object>((src, dest) =>
        //        {
        //            var request = src as RespondToAdvisoringContractRequestDto;
        //            var advRequest = dest as AdvisoringRequest;
        //            if (request != null && advRequest != null)
        //            {
        //                advRequest.AdvisoringRequestStatus = request.ResponseStatus;
        //                advRequest.ResponseAdvisor = request.ResponseAdvisor;
        //            }
        //        });

        //    // Setup contract repository
        //    _advisoringContractRepositoryMock.Setup(c => c.AddAsync(It.IsAny<AdvisoringContract>()))
        //        .ReturnsAsync((AdvisoringContract contract) =>
        //        {
        //            contract.Id = 1;
        //            return contract;
        //        });

        //    // Setup email service
        //    _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()))
        //        .Returns(Task.FromResult(true)); // Use Returns with Task.FromResult instead of ReturnsAsync

        //    // Setup message service
        //    _messageServiceMock.Setup(m => m.GetMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns("Test email template");

        //    var handler = new RespondToAdvisoringContractCommandHandler(
        //        _mapperMock.Object,
        //        _unitOfWorkMock.Object,
        //        _emailServiceMock.Object,
        //        _messageServiceMock.Object,
        //        _loggerMock.Object);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.Succeeded);
        //    Assert.True(result.Data);

        //    // Verify student's research group was updated
        //    _actorRepositoryMock.Verify(a => a.UpdateAsync(It.Is<Actor>(s => s.Id == 2 && s.ParentId == 1)), Times.Once);
        //    _advisoringContractRepositoryMock.Verify(c => c.AddAsync(It.IsAny<AdvisoringContract>()), Times.Once);
        //    _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<GestionAsesoria.Operator.Application.DTOs.Mail.Request.MailRequest>()), Times.Once);
        //}

        //#endregion
    }
}