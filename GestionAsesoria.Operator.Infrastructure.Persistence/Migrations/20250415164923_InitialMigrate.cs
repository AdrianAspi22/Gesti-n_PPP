using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "ActorType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdLabel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre del estado de la cita."),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se asigna el estado."),
                    StateId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado."),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Comentario adicional sobre el estado de la cita.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatus", x => x.Id);
                },
                comment: "Representa los estados de una cita, el cual incluye el nombre del estado, la fecha y comentarios adicionales.");

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoogleCalendarEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleCalendarEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Código único que identifica el dato maestro en el sistema. Este código es utilizado para referenciar el dato de manera única."),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre descriptivo del dato maestro. Este nombre es usado para identificar el dato en interfaces y reportes."),
                    TypeId = table.Column<int>(type: "int", nullable: false, comment: "Tipo del dato maestro, referenciado por su identificador (por ejemplo, tipo de datos como 'categoría', 'subcategoría', etc.).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterData", x => x.Id);
                },
                comment: "Representa los datos maestros utilizados en el sistema, como códigos y tipos de datos relacionados.");

            migrationBuilder.CreateTable(
                name: "Mentoring",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentoring", x => x.Id);
                },
                comment: "Representa una Tutoría, incluyendo los contratos de tutoría asociadas.");

            migrationBuilder.CreateTable(
                name: "PreProfessionalInternship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreProfessionalInternship", x => x.Id);
                },
                comment: "Representa una Práctica Pre Profesional, incluyendo los contratos de Prácticas Pre Profesionales asociadas.");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Role_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAppointmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre del estado actual de la cita."),
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAppointmentStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAppointmentStatus_AppointmentStatus_AppointmentStatusId",
                        column: x => x.AppointmentStatusId,
                        principalTable: "AppointmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa el estado actual de una cita.");

            migrationBuilder.CreateTable(
                name: "BusinessSettingParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessSettingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSettingParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessSettingParameter_BusinessSetting_BusinessSettingId",
                        column: x => x.BusinessSettingId,
                        principalTable: "BusinessSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterDataValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Código único que identifica el valor dentro de los datos maestros. Este código es usado para referenciar el valor de manera única."),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre descriptivo del valor. Este nombre se utiliza para mostrar el valor en interfaces y reportes."),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Valor asociado al dato maestro. Puede ser cualquier tipo de dato como una cadena, número o booleano dependiendo del tipo de dato maestro."),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en que se creó el valor. Se utiliza para controlar cuándo fue insertado el valor en el sistema."),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de la última modificación del valor. Permite conocer cuándo se actualizó por última vez el registro."),
                    MasterDataId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del dato maestro al que pertenece este valor. Es una clave foránea que refiere a la entidad MasterData."),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Indica si el valor está activo o inactivo. Es útil para desactivar valores sin eliminarlos permanentemente."),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Descripción adicional sobre el valor. Se utiliza para proporcionar más información acerca del valor y su contexto.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterDataValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterDataValue_MasterData_MasterDataId",
                        column: x => x.MasterDataId,
                        principalTable: "MasterData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa los valores asociados a los datos maestros en el sistema. Cada valor tiene un código, nombre y una descripción.");

            migrationBuilder.CreateTable(
                name: "RoleByActorType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ActorTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleByActorType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleByActorType_ActorType_ActorTypeId",
                        column: x => x.ActorTypeId,
                        principalTable: "ActorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleByActorType_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de la cita."),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Ubicación de la cita."),
                    CurrentAppointmentStatusId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado actual de la cita.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_CurrentAppointmentStatus_CurrentAppointmentStatusId",
                        column: x => x.CurrentAppointmentStatusId,
                        principalTable: "CurrentAppointmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Permite crear una cita, incluyendo detalles sobre la fecha, ubicación y estado actual.");

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Dependerá del contexto del Actor, si es persona Natural será el Nombre de la persona, si es persona jurídica será la Razón Social, si es entidad interna, llevará el nombre de la entidad como por ejemplo para actor Grupo de Investigación, Grupo de Investigación de Ingeniería de Software"),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Dependerá del contexto del Actor, si es persona Natural será el Apellido de la persona, si es persona jurídica será la Nombre Comercial, si es entidad interna, llevará el Acrónimo de la entidad como por ejemplo para actor Grupo de Investigación, su acrónimo será GINSOFT"),
                    ThirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassifyActor = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Es un clasificador para seleccionar si es persona Natural será su género ejemplo: Masculino y Femenino, si es persona Jurídica se seleccionará: SAC, EIRL, Etc"),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    CodeORCID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTIVitaeCONCYTEC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HigherDegree = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mayor Grado Académico"),
                    CalendarSettings = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Configuraciones del calendario del actor, se guardará mediante una cadena de texto y estará separado por comas (por ejemplo 12, 14, 15), esta máscara vendra desde el front. Donde: 12--> IdCalendarioTesis   14--> IdCalendarioPPP   15--> IdCalendarioTutoría"),
                    IdentificationTypeId = table.Column<int>(type: "int", nullable: true),
                    ActorTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    MainRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actor_ActorType_ActorTypeId",
                        column: x => x.ActorTypeId,
                        principalTable: "ActorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actor_Actor_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actor_MasterDataValue_IdentificationTypeId",
                        column: x => x.IdentificationTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actor_Role_MainRoleId",
                        column: x => x.MainRoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                },
                comment: "Representa a un actor en el sistema, el cual está involucrada en contratos de asesoría.");

            migrationBuilder.CreateTable(
                name: "ActorSecondaryRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorSecondaryRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorSecondaryRole_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActorSecondaryRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvisoringRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRequest = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se realiza la solicitud de asesoría."),
                    DateResponseAdvisor = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha en la que se realiza la Respuesta del asesor hacia la petición del Contrato de Asesoría, puede ser nula en una primera instancia"),
                    UserSubject = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Asunto de la razón de la solicitud del usuario"),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mensaje del usuario explicando la razón de la solicitud."),
                    ResponseAdvisor = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Respuesta del asesor a la solicitud."),
                    AdvisoringRequestStatus = table.Column<int>(type: "int", nullable: false, comment: "Estado actual de la solicitud de asesoría."),
                    RequesterActorId = table.Column<int>(type: "int", nullable: false),
                    AdvisorActorId = table.Column<int>(type: "int", nullable: false),
                    UserActorId = table.Column<int>(type: "int", nullable: false),
                    ResearchGroupId = table.Column<int>(type: "int", nullable: true),
                    ResearchLineId = table.Column<int>(type: "int", nullable: true),
                    ResearchAreaId = table.Column<int>(type: "int", nullable: true),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisoringRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_AdvisorActorId",
                        column: x => x.AdvisorActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_RequesterActorId",
                        column: x => x.RequesterActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_ResearchAreaId",
                        column: x => x.ResearchAreaId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_ResearchGroupId",
                        column: x => x.ResearchGroupId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_ResearchLineId",
                        column: x => x.ResearchLineId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_Actor_UserActorId",
                        column: x => x.UserActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringRequest_MasterDataValue_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Representa una solicitud de asesoría, incluyendo detalles sobre el mensaje del usuario, la respuesta del asesor y el estado de la solicitud.");

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserUri = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "URI del usuario asociado al calendario."),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Token de acceso para el calendario."),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Token de actualización para el calendario."),
                    AccessTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de expiración del token de acceso, puede ser nula si no se ha establecido."),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de expiración del token de actualización, puede ser nula si no se ha establecido."),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Tipo de evento asociado al calendario, puede ser nulo."),
                    EventsPageToken = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Token de la página de eventos, puede ser nulo."),
                    SchedulingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "URL de programación del calendario, puede ser nula."),
                    ActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del actor asociado al calendario.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendar_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa el calendario que se integrará de Google, el cual incluye detalles sobre tokens de acceso, tipo de eventos y URL de programación.");

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de inicio de la membresía."),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de finalización de la membresía, puede ser nula si aún está activo."),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    OrganizationActorId = table.Column<int>(type: "int", nullable: false),
                    MembershipTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Membership_Actor_OrganizationActorId",
                        column: x => x.OrganizationActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Membership_MasterDataValue_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Representa un miembro de un grupo de investigación, incluyendo detalles sobre su rol y las tesis asociadas.");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActorId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePictureDataUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvisoringContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Asunto, se refiere al título del tema principal a la cual se va a consultar al remitente"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Se explica a detalle el motivo por el cual se está realizando el contrato"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de registro del contrato"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de finalización del contrato, puede ser nula si el contrato está en curso"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Este atributo nos permite verificar si el usuario cuenta con un contrato activo"),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Número de contrato"),
                    ContractStatusId = table.Column<int>(type: "int", nullable: false, comment: "Estado actual del contrato, relacionado con MasterDataValue."),
                    StudentId = table.Column<int>(type: "int", nullable: false, comment: "Entidad Actor asociado al contrato de Asesoría"),
                    AdvisorId = table.Column<int>(type: "int", nullable: false, comment: "Entidad Actor asociado al contrato de Asesoría"),
                    ResearchGroupId = table.Column<int>(type: "int", nullable: true, comment: "Tipo de contrato, relacionado con MasterDataValue."),
                    ResearchLineId = table.Column<int>(type: "int", nullable: true, comment: "Tipo de contrato, relacionado con MasterDataValue."),
                    ResearchAreaId = table.Column<int>(type: "int", nullable: true, comment: "Tipo de contrato, relacionado con MasterDataValue."),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false, comment: "Tipo de contrato, relacionado con MasterDataValue."),
                    AdvisoringRequestId = table.Column<int>(type: "int", nullable: true, comment: "Entidad de Solicitud de Asesoría asociado al contrato de Asesoría"),
                    ActorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisoringContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_ResearchAreaId",
                        column: x => x.ResearchAreaId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_ResearchGroupId",
                        column: x => x.ResearchGroupId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_ResearchLineId",
                        column: x => x.ResearchLineId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_Actor_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_AdvisoringRequest_AdvisoringRequestId",
                        column: x => x.AdvisoringRequestId,
                        principalTable: "AdvisoringRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_MasterDataValue_ContractStatusId",
                        column: x => x.ContractStatusId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisoringContract_MasterDataValue_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contrato de Asesoria, en el cual se asume responsabilidades entre el asesor y asesorado por un tiempo limitado");

            migrationBuilder.CreateTable(
                name: "Thesis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Título de la tesis."),
                    TesistaMembershipId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del miembro del grupo que es tesista."),
                    MainAdvisorMembershipId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del miembro del grupo que es asesor principal."),
                    Secondary1AdvisorMembershipId = table.Column<int>(type: "int", nullable: true, comment: "Identificador del miembro del grupo que es asesor secundario 1, puede ser nulo."),
                    Secondary2AdvisorMembershipId = table.Column<int>(type: "int", nullable: true, comment: "Identificador del miembro del grupo que es asesor secundario 2, puede ser nulo.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thesis_Membership_MainAdvisorMembershipId",
                        column: x => x.MainAdvisorMembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Thesis_Membership_Secondary1AdvisorMembershipId",
                        column: x => x.Secondary1AdvisorMembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Thesis_Membership_Secondary2AdvisorMembershipId",
                        column: x => x.Secondary2AdvisorMembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Thesis_Membership_TesistaMembershipId",
                        column: x => x.TesistaMembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Representa una tesis, incluyendo detalles sobre el título, asesores y líneas de investigación asociadas.");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvisingSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nota adicional sobre la sesión de asesoría, el \"?\" nos dice que es opcional llenar ese campo"),
                    AdvisoringContractId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisingSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvisingSession_AdvisoringContract_AdvisoringContractId",
                        column: x => x.AdvisoringContractId,
                        principalTable: "AdvisoringContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvisingSession_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa una sesión de asesoría, que incluye tareas y notas relacionadas con una cita específica.");

            migrationBuilder.CreateTable(
                name: "DocumentCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Ruta del archivo en el sistema."),
                    FileSize = table.Column<long>(type: "bigint", nullable: false, comment: "Tamaño de archivo)."),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se subió el documento."),
                    OnlineUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "URL pública del documento en línea."),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del tipo de documento (MasterDataValue)."),
                    UploadedByActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del usuario que subió el documento (Actor)."),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCollection_Actor_UploadedByActorId",
                        column: x => x.UploadedByActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentCollection_AdvisoringContract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "AdvisoringContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentCollection_MasterDataValue_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MentoringByAdvisoringContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentoringId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la tutoría asociada."),
                    AdvisoringContractId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del contrato de asesoría asociado."),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Este atributo nos permite verificar si el usuario cuenta con un contrato de Tutoría activo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentoringByAdvisoringContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentoringByAdvisoringContract_AdvisoringContract_AdvisoringContractId",
                        column: x => x.AdvisoringContractId,
                        principalTable: "AdvisoringContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentoringByAdvisoringContract_Mentoring_MentoringId",
                        column: x => x.MentoringId,
                        principalTable: "Mentoring",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Es una tabla intermedia el cual representa un contrato de Tutoría, incluyendo referencias a la tutoría y al contrato de asesoría asociados.");

            migrationBuilder.CreateTable(
                name: "PreProfessionalInternshipByAdvisoringContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreProfessionalInternshipId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la Práctica Pre Profesional asociada."),
                    AdvisoringContractId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del contrato de asesoría asociado."),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Este atributo nos permite verificar si el usuario cuenta con un contrato de PPP activo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreProfessionalInternshipByAdvisoringContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreProfessionalInternshipByAdvisoringContract_AdvisoringContract_AdvisoringContractId",
                        column: x => x.AdvisoringContractId,
                        principalTable: "AdvisoringContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreProfessionalInternshipByAdvisoringContract_PreProfessionalInternship_PreProfessionalInternshipId",
                        column: x => x.PreProfessionalInternshipId,
                        principalTable: "PreProfessionalInternship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Es una tabla intermedia el cual representa un contrato de Práctica Pre Profesional, incluyendo referencias a la Práctica Pre Profesional y al contrato de asesoría asociados.");

            migrationBuilder.CreateTable(
                name: "ThesisByAdvisoringContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la tesis asociada."),
                    AdvisoringContractId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del contrato de asesoría asociado."),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Este atributo nos permite verificar si el usuario cuenta con un contrato de Tesis activo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisByAdvisoringContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesisByAdvisoringContract_AdvisoringContract_AdvisoringContractId",
                        column: x => x.AdvisoringContractId,
                        principalTable: "AdvisoringContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThesisByAdvisoringContract_Thesis_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Thesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa un contrato de tesis, el cual representa una tabla intermedia donde incluye referencias a la tesis y al contrato de asesoría asociados.");

            migrationBuilder.CreateTable(
                name: "ThesisStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se emite el estado."),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Archivo asociado al estado de la tesis."),
                    ThesisId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la tesis asociada."),
                    ThesisStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesisStatusHistory_MasterDataValue_ThesisStatusId",
                        column: x => x.ThesisStatusId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThesisStatusHistory_Thesis_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Thesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa el historial de estado de una tesis, incluyendo detalles sobre el estado, fecha de emisión y archivo asociado.");

            migrationBuilder.CreateTable(
                name: "AdvisoringTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre de la tarea de asesoría."),
                    AttachmentsJson = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Archivos adjuntos en formato JSON."),
                    AdvisorExplanation = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Explicación del asesor sobre la tarea."),
                    StudentResponse = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Respuesta del estudiante a la tarea."),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Revisión de la tarea, puede ser nula si aún no se ha revisado, eso es lo que indica el símbolo \"?\" "),
                    AdvisingSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisoringTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvisoringTask_AdvisingSession_AdvisingSessionId",
                        column: x => x.AdvisingSessionId,
                        principalTable: "AdvisingSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Representa una tarea de asesoría, que incluye detalles sobre la tarea asignada, explicaciones del Asesor y respuestas del estudiante.");

            migrationBuilder.CreateTable(
                name: "DocumentVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentCollectionId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentVersion_DocumentCollection_DocumentCollectionId",
                        column: x => x.DocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Título o nombre del proyecto."),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de inicio del proyecto."),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de finalización del proyecto."),
                    ExecutionPlace = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Lugar donde se ejecuta el proyecto."),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "Estado activo del proyecto (true = activo; false = inactivo)."),
                    ResearchGroupProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del grupo de investigación asociado al proyecto (Actor)."),
                    ResearchAreaProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del área de investigación vinculada (Actor)."),
                    ResearchLineProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la línea de investigación correspondiente (Actor)."),
                    MethodProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del método aplicado en la ejecución del proyecto (MasterDataValue)."),
                    OdsObjectiveId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del obejtivo ODS para el proyecto (MasterDataValue)."),
                    ClassificationProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la clasificación asignada al proyecto (MasterDataValue)."),
                    StateProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado asignado al proyecto (MasterDataValue)."),
                    AuthorProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del autor del proyecto (Actor)."),
                    ResolutionDocumentCollectionId = table.Column<int>(type: "int", nullable: true, comment: "Identificador de la colección de documentos de resolución (DocumentCollection)."),
                    PlanDocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos del plan (DocumentCollection)."),
                    ReportAdvisorDocumentCollectionId = table.Column<int>(type: "int", nullable: true, comment: "Identificador de la colección de documentos del dictamen del asesor (DocumentCollection)."),
                    ReportOfResearchGroupDocumentCollectionId = table.Column<int>(type: "int", nullable: true, comment: "Identificador de la colección de documentos del dictamen o acuerdo del grupo de investigación (DocumentCollection)."),
                    ReportOfDegreesAndTittlesCommitteeId = table.Column<int>(type: "int", nullable: true, comment: "Identificador de la colección de documentos del acuerdo de la comisión de grados y títulos")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Actor_AuthorProjectId",
                        column: x => x.AuthorProjectId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Actor_ResearchAreaProjectId",
                        column: x => x.ResearchAreaProjectId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Actor_ResearchGroupProjectId",
                        column: x => x.ResearchGroupProjectId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Actor_ResearchLineProjectId",
                        column: x => x.ResearchLineProjectId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_DocumentCollection_PlanDocumentCollectionId",
                        column: x => x.PlanDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_DocumentCollection_ReportAdvisorDocumentCollectionId",
                        column: x => x.ReportAdvisorDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_DocumentCollection_ReportOfDegreesAndTittlesCommitteeId",
                        column: x => x.ReportOfDegreesAndTittlesCommitteeId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_DocumentCollection_ReportOfResearchGroupDocumentCollectionId",
                        column: x => x.ReportOfResearchGroupDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_DocumentCollection_ResolutionDocumentCollectionId",
                        column: x => x.ResolutionDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_MasterDataValue_ClassificationProjectId",
                        column: x => x.ClassificationProjectId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_MasterDataValue_MethodProjectId",
                        column: x => x.MethodProjectId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_MasterDataValue_OdsObjectiveId",
                        column: x => x.OdsObjectiveId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_MasterDataValue_StateProjectId",
                        column: x => x.StateProjectId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Advance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvanceDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha del avance del proyecto."),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Descripción del avance."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del proyecto al que pertenece este avance."),
                    DeliverableDocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos entregables (DocumentCollection)."),
                    ReportOfResearchGroupDocumentCollectionId = table.Column<int>(type: "int", nullable: true, comment: "Identificador de la colección de documentos del informe del grupo de investigación (DocumentCollection).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advance_DocumentCollection_DeliverableDocumentCollectionId",
                        column: x => x.DeliverableDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advance_DocumentCollection_ReportOfResearchGroupDocumentCollectionId",
                        column: x => x.ReportOfResearchGroupDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advance_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Monto asignado al financiamiento."),
                    FundingType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Tipo de financiamiento Propio, FIF, etc)."),
                    IsCompetitiveFund = table.Column<bool>(type: "bit", nullable: false, comment: "Indica si el financiamiento participa en un fondo concursable."),
                    FundingName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre del fondo concursable (si aplica)."),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Nombre de la organización asociada al fondo (si aplica)."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del proyecto asociado al financiamiento.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funding_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Justificación para la participación del actor en el proyecto."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del proyecto asociado al actor."),
                    ActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del actor que participa en el proyecto (Actor)."),
                    AuthorTypeId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del tipo de autor dentro del proyecto (MasterDataValue).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActor_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActor_MasterDataValue_AuthorTypeId",
                        column: x => x.AuthorTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActor_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se realizó la evaluación."),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Comentarios realizados por el evaluador."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del proyecto evaluado."),
                    ActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del evaluador (Actor)."),
                    StatusEvaluationId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado de la evaluación (MasterDataValue)."),
                    DocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos asociados a la evaluación (DocumentCollection).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluation_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluation_DocumentCollection_DocumentCollectionId",
                        column: x => x.DocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluation_MasterDataValue_StatusEvaluationId",
                        column: x => x.StatusEvaluationId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluation_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Título de la producción científica."),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de publicación de la producción científica."),
                    Doi = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Identificador DOI de la producción científica."),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Código de la producción científica."),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creación del registro."),
                    ProjectId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del proyecto asociado a la producción científica."),
                    ProductionTypeId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del tipo de producción científica (MasterDataValue)."),
                    DeliverableDocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos entregables (DocumentCollection).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificProduction_DocumentCollection_DeliverableDocumentCollectionId",
                        column: x => x.DeliverableDocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificProduction_MasterDataValue_ProductionTypeId",
                        column: x => x.ProductionTypeId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificProduction_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se realizó la evaluación del avance."),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Comentarios realizados por el evaluador."),
                    AdvanceId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del avance evaluado."),
                    ActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del evaluador (Actor)."),
                    StatusEvaluationId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado de la evaluación (MasterDataValue)."),
                    DocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos asociados a la evaluación (DocumentCollection).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvanceEvaluation_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceEvaluation_Advance_AdvanceId",
                        column: x => x.AdvanceId,
                        principalTable: "Advance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceEvaluation_DocumentCollection_DocumentCollectionId",
                        column: x => x.DocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceEvaluation_MasterDataValue_StatusEvaluationId",
                        column: x => x.StatusEvaluationId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificProductionEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha en la que se realizó la evaluación."),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Comentarios realizados por el evaluador."),
                    ScientificProductionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la producción científica evaluada."),
                    ActorId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del evaluador (Actor)."),
                    StatusEvaluationId = table.Column<int>(type: "int", nullable: false, comment: "Identificador del estado de la evaluación (MasterDataValue)."),
                    DocumentCollectionId = table.Column<int>(type: "int", nullable: false, comment: "Identificador de la colección de documentos asociados a la evaluación (DocumentCollection).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificProductionEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificProductionEvaluation_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificProductionEvaluation_DocumentCollection_DocumentCollectionId",
                        column: x => x.DocumentCollectionId,
                        principalTable: "DocumentCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificProductionEvaluation_MasterDataValue_StatusEvaluationId",
                        column: x => x.StatusEvaluationId,
                        principalTable: "MasterDataValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScientificProductionEvaluation_ScientificProduction_ScientificProductionId",
                        column: x => x.ScientificProductionId,
                        principalTable: "ScientificProduction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_ActorTypeId",
                table: "Actor",
                column: "ActorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actor_IdentificationTypeId",
                table: "Actor",
                column: "IdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MainRoleId",
                table: "Actor",
                column: "MainRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Actor_ParentId",
                table: "Actor",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorSecondaryRole_ActorId_RoleId",
                table: "ActorSecondaryRole",
                columns: new[] { "ActorId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActorSecondaryRole_RoleId",
                table: "ActorSecondaryRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Advance_DeliverableDocumentCollectionId",
                table: "Advance",
                column: "DeliverableDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advance_ProjectId",
                table: "Advance",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Advance_ReportOfResearchGroupDocumentCollectionId",
                table: "Advance",
                column: "ReportOfResearchGroupDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceEvaluation_ActorId",
                table: "AdvanceEvaluation",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceEvaluation_AdvanceId",
                table: "AdvanceEvaluation",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceEvaluation_DocumentCollectionId",
                table: "AdvanceEvaluation",
                column: "DocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceEvaluation_StatusEvaluationId",
                table: "AdvanceEvaluation",
                column: "StatusEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisingSession_AdvisoringContractId",
                table: "AdvisingSession",
                column: "AdvisoringContractId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisingSession_AppointmentId",
                table: "AdvisingSession",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ActorId",
                table: "AdvisoringContract",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_AdvisorId",
                table: "AdvisoringContract",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_AdvisoringRequestId",
                table: "AdvisoringContract",
                column: "AdvisoringRequestId",
                unique: true,
                filter: "[AdvisoringRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ContractStatusId",
                table: "AdvisoringContract",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ResearchAreaId",
                table: "AdvisoringContract",
                column: "ResearchAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ResearchGroupId",
                table: "AdvisoringContract",
                column: "ResearchGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ResearchLineId",
                table: "AdvisoringContract",
                column: "ResearchLineId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_ServiceTypeId",
                table: "AdvisoringContract",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringContract_StudentId",
                table: "AdvisoringContract",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_AdvisorActorId",
                table: "AdvisoringRequest",
                column: "AdvisorActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_RequesterActorId",
                table: "AdvisoringRequest",
                column: "RequesterActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_ResearchAreaId",
                table: "AdvisoringRequest",
                column: "ResearchAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_ResearchGroupId",
                table: "AdvisoringRequest",
                column: "ResearchGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_ResearchLineId",
                table: "AdvisoringRequest",
                column: "ResearchLineId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_ServiceTypeId",
                table: "AdvisoringRequest",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringRequest_UserActorId",
                table: "AdvisoringRequest",
                column: "UserActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvisoringTask_AdvisingSessionId",
                table: "AdvisoringTask",
                column: "AdvisingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CurrentAppointmentStatusId",
                table: "Appointment",
                column: "CurrentAppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSettingParameter_BusinessSettingId",
                table: "BusinessSettingParameter",
                column: "BusinessSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_ActorId",
                table: "Calendar",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAppointmentStatus_AppointmentStatusId",
                table: "CurrentAppointmentStatus",
                column: "AppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCollection_ContractId",
                table: "DocumentCollection",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCollection_DocumentTypeId",
                table: "DocumentCollection",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCollection_UploadedByActorId",
                table: "DocumentCollection",
                column: "UploadedByActorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersion_DocumentCollectionId",
                table: "DocumentVersion",
                column: "DocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Funding_ProjectId",
                table: "Funding",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataValue_MasterDataId",
                table: "MasterDataValue",
                column: "MasterDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_ActorId",
                table: "Membership",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_MembershipTypeId",
                table: "Membership",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_OrganizationActorId",
                table: "Membership",
                column: "OrganizationActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MentoringByAdvisoringContract_AdvisoringContractId",
                table: "MentoringByAdvisoringContract",
                column: "AdvisoringContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MentoringByAdvisoringContract_MentoringId",
                table: "MentoringByAdvisoringContract",
                column: "MentoringId");

            migrationBuilder.CreateIndex(
                name: "IX_PreProfessionalInternshipByAdvisoringContract_AdvisoringContractId",
                table: "PreProfessionalInternshipByAdvisoringContract",
                column: "AdvisoringContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PreProfessionalInternshipByAdvisoringContract_PreProfessionalInternshipId",
                table: "PreProfessionalInternshipByAdvisoringContract",
                column: "PreProfessionalInternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_AuthorProjectId",
                table: "Project",
                column: "AuthorProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClassificationProjectId",
                table: "Project",
                column: "ClassificationProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MethodProjectId",
                table: "Project",
                column: "MethodProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_OdsObjectiveId",
                table: "Project",
                column: "OdsObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PlanDocumentCollectionId",
                table: "Project",
                column: "PlanDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ReportAdvisorDocumentCollectionId",
                table: "Project",
                column: "ReportAdvisorDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ReportOfDegreesAndTittlesCommitteeId",
                table: "Project",
                column: "ReportOfDegreesAndTittlesCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ReportOfResearchGroupDocumentCollectionId",
                table: "Project",
                column: "ReportOfResearchGroupDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ResearchAreaProjectId",
                table: "Project",
                column: "ResearchAreaProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ResearchGroupProjectId",
                table: "Project",
                column: "ResearchGroupProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ResearchLineProjectId",
                table: "Project",
                column: "ResearchLineProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ResolutionDocumentCollectionId",
                table: "Project",
                column: "ResolutionDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StateProjectId",
                table: "Project",
                column: "StateProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActor_ActorId",
                table: "ProjectActor",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActor_AuthorTypeId",
                table: "ProjectActor",
                column: "AuthorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActor_ProjectId",
                table: "ProjectActor",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluation_ActorId",
                table: "ProjectEvaluation",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluation_DocumentCollectionId",
                table: "ProjectEvaluation",
                column: "DocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluation_ProjectId",
                table: "ProjectEvaluation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluation_StatusEvaluationId",
                table: "ProjectEvaluation",
                column: "StatusEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ParentId",
                table: "Role",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleByActorType_ActorTypeId",
                table: "RoleByActorType",
                column: "ActorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleByActorType_RoleId",
                table: "RoleByActorType",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProduction_DeliverableDocumentCollectionId",
                table: "ScientificProduction",
                column: "DeliverableDocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProduction_ProductionTypeId",
                table: "ScientificProduction",
                column: "ProductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProduction_ProjectId",
                table: "ScientificProduction",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProductionEvaluation_ActorId",
                table: "ScientificProductionEvaluation",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProductionEvaluation_DocumentCollectionId",
                table: "ScientificProductionEvaluation",
                column: "DocumentCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProductionEvaluation_ScientificProductionId",
                table: "ScientificProductionEvaluation",
                column: "ScientificProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificProductionEvaluation_StatusEvaluationId",
                table: "ScientificProductionEvaluation",
                column: "StatusEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_MainAdvisorMembershipId",
                table: "Thesis",
                column: "MainAdvisorMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_Secondary1AdvisorMembershipId",
                table: "Thesis",
                column: "Secondary1AdvisorMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_Secondary2AdvisorMembershipId",
                table: "Thesis",
                column: "Secondary2AdvisorMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_TesistaMembershipId",
                table: "Thesis",
                column: "TesistaMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisByAdvisoringContract_AdvisoringContractId",
                table: "ThesisByAdvisoringContract",
                column: "AdvisoringContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisByAdvisoringContract_ThesisId",
                table: "ThesisByAdvisoringContract",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisStatusHistory_ThesisId",
                table: "ThesisStatusHistory",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisStatusHistory_ThesisStatusId",
                table: "ThesisStatusHistory",
                column: "ThesisStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActorId",
                schema: "Identity",
                table: "Users",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorSecondaryRole");

            migrationBuilder.DropTable(
                name: "AdvanceEvaluation");

            migrationBuilder.DropTable(
                name: "AdvisoringTask");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "BusinessSettingParameter");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "DocumentVersion");

            migrationBuilder.DropTable(
                name: "Funding");

            migrationBuilder.DropTable(
                name: "GoogleCalendarEvent");

            migrationBuilder.DropTable(
                name: "MentoringByAdvisoringContract");

            migrationBuilder.DropTable(
                name: "PreProfessionalInternshipByAdvisoringContract");

            migrationBuilder.DropTable(
                name: "ProjectActor");

            migrationBuilder.DropTable(
                name: "ProjectEvaluation");

            migrationBuilder.DropTable(
                name: "RoleByActorType");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ScientificProductionEvaluation");

            migrationBuilder.DropTable(
                name: "ThesisByAdvisoringContract");

            migrationBuilder.DropTable(
                name: "ThesisStatusHistory");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Advance");

            migrationBuilder.DropTable(
                name: "AdvisingSession");

            migrationBuilder.DropTable(
                name: "BusinessSetting");

            migrationBuilder.DropTable(
                name: "Mentoring");

            migrationBuilder.DropTable(
                name: "PreProfessionalInternship");

            migrationBuilder.DropTable(
                name: "ScientificProduction");

            migrationBuilder.DropTable(
                name: "Thesis");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "CurrentAppointmentStatus");

            migrationBuilder.DropTable(
                name: "DocumentCollection");

            migrationBuilder.DropTable(
                name: "AppointmentStatus");

            migrationBuilder.DropTable(
                name: "AdvisoringContract");

            migrationBuilder.DropTable(
                name: "AdvisoringRequest");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "ActorType");

            migrationBuilder.DropTable(
                name: "MasterDataValue");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "MasterData");
        }
    }
}
