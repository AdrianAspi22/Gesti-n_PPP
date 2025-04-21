using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Domain.Entities.Identity;
using GestionAsesoria.Operator.Domain.Entities.Parameters;
using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using GestionAsesoria.Operator.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : AuditableContext
{
    private readonly IDateTimeService? _dateTime;
    private readonly ICurrentUserService? _currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDateTimeService? dateTime = null, ICurrentUserService? currentUserService = null) : base(options)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    // Constructor para tiempo de diseño
    public ApplicationDbContext() : base(GetOptions())
    {
    }

    private static DbContextOptions<ApplicationDbContext> GetOptions()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        
        return optionsBuilder.Options;
    }

    public virtual DbSet<BusinessSetting> BusinessSetting { get; set; } = null!;
    public virtual DbSet<BusinessSettingParameter> BusinessSettingParameter { get; set; } = null!;


    // DbSets para las entidades principales
    public virtual DbSet<Project> Project { get; set; } = null!;
    public virtual DbSet<Advance> Advance { get; set; } = null!;
    public virtual DbSet<ScientificProduction> ScientificProduction { get; set; } = null!;
    public virtual DbSet<DocumentCollection> DocumentCollection { get; set; } = null!;
    public virtual DbSet<DocumentVersion> DocumentVersion { get; set; } = null!;
    public virtual DbSet<Funding> Funding { get; set; } = null!;

    // DbSets para las tablas intermedias (relaciones muchos a muchos)
    public virtual DbSet<ProjectActor> ProjectActor { get; set; } = null!;
    public virtual DbSet<ProjectEvaluation> ProjectEvaluation { get; set; } = null!;
    public virtual DbSet<AdvanceEvaluation> AdvanceEvaluation { get; set; } = null!;
    public virtual DbSet<ScientificProductionEvaluation> ScientificProductionEvaluation { get; set; } = null!;


    public virtual DbSet<Actor> Actor { get; set; } = null!;
    public virtual DbSet<ActorType> ActorType { get; set; } = null!;
    public virtual DbSet<ActorSecondaryRole> ActorSecondaryRole { get; set; } = null!;


    public virtual DbSet<AdvisingSession> AdvisingSession { get; set; } = null!;
    public virtual DbSet<AdvisoringContract> AdvisoringContract { get; set; } = null!;
    public virtual DbSet<AdvisoringRequest> AdvisoringRequest { get; set; } = null!;
    public virtual DbSet<AdvisoringTask> AdvisoringTask { get; set; } = null!;

    public virtual DbSet<Appointment> Appointment { get; set; } = null!;
    public virtual DbSet<AppointmentStatus> AppointmentStatus { get; set; } = null!;

    public virtual DbSet<Calendar> Calendar { get; set; } = null!;
    public virtual DbSet<CurrentAppointmentStatus> CurrentAppointmentStatus { get; set; } = null!;
    public virtual DbSet<GoogleCalendarEvent> GoogleCalendarEvent { get; set; } = null;

    public virtual DbSet<Membership> Membership { get; set; } = null!;
    public virtual DbSet<MasterData> MasterData { get; set; } = null!;
    public virtual DbSet<MasterDataValue> MasterDataValue { get; set; } = null!;

    public virtual DbSet<Mentoring> Mentoring { get; set; } = null!;
    public virtual DbSet<MentoringByAdvisoringContract> MentoringByAdvisoringContract { get; set; } = null!;
    public virtual DbSet<PreProfessionalInternship> PreProfessionalInternship { get; set; } = null!;
    public virtual DbSet<PreProfessionalInternshipByAdvisoringContract> PreProfessionalInternshipByAdvisoringContract { get; set; } = null!;

    public virtual DbSet<Role> Role { get; set; } = null!;
    public virtual DbSet<RoleByActorType> RoleByActorType { get; set; } = null!;

    public virtual DbSet<Thesis> Thesis { get; set; } = null!;
    public virtual DbSet<ThesisByAdvisoringContract> ThesisByAdvisoringContract { get; set; } = null!;
    public virtual DbSet<ThesisStatusHistory> ThesisStatusHistory { get; set; } = null!;


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        if (_currentUserService.UserId == null)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        else
        {
            return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Configuración decimal
        foreach (var property in builder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name is "LastModifiedBy" or "CreatedBy"))
        {
            property.SetColumnType("nvarchar(128)");
        }

        base.OnModelCreating(builder);

        // Configuración de Identity
        builder.Entity<AcademicUser>(entity =>
        {
            entity.ToTable(name: "Users", "Identity");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        builder.Entity<AcademicRole>(entity =>
        {
            entity.ToTable(name: "Roles", "Identity");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles", "Identity");
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims", "Identity");
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins", "Identity");
        });

        builder.Entity<AcademicRoleClaim>(entity =>
        {
            entity.ToTable(name: "RoleClaims", "Identity");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens", "Identity");
        });

        // Relaciones de las entidades Academicas

        builder.Entity<Membership>(entity =>
        {
            // Configuración para el Actor miembro
            entity.HasOne(m => m.MemberActor)
                .WithMany(a => a.Memberships)
                .HasForeignKey(m => m.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para el Actor organización
            entity.HasOne(m => m.OrganizationActor)
                .WithMany()
                .HasForeignKey(m => m.OrganizationActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para MembershipType
            entity.HasOne(m => m.MembershipType)
                .WithMany()
                .HasForeignKey(m => m.MembershipTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Role>(entity =>
        {
            builder.Entity<Role>(entity =>
            {
                entity.HasOne(r => r.Parent)
                .WithMany(r => r.Children)
                .HasForeignKey(r => r.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            });

        });

        builder.Entity<Actor>(entity =>
        {
            // Relación con Role
            entity.HasOne(r => r.Parent)
            .WithMany()
            .HasForeignKey(r => r.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

            // Relación con Actortype
            entity.HasOne(a => a.ActorType)
            .WithMany()
            .HasForeignKey(a => a.ActorTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AdvisoringRequest>(entity =>
        {
            // Relación con Actor
            entity.HasOne(r => r.AdvisorActor)
            .WithOne()
            .HasForeignKey<AdvisoringRequest>(r => r.AdvisorActorId)
            .OnDelete(DeleteBehavior.Restrict);


            // Relación con Role
            entity.HasOne(r => r.UserActor)
            .WithOne()
            .HasForeignKey<AdvisoringRequest>(r => r.UserActorId)
            .OnDelete(DeleteBehavior.Restrict);

            // Relación con Role
            entity.HasOne(r => r.ServiceType)
            .WithOne()
            .HasForeignKey<AdvisoringRequest>(r => r.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AdvisoringContract>(entity =>
        {
            // Relación con Actor
            entity.HasOne(r => r.AdvisoringRequest)
            .WithOne()
            .HasForeignKey<AdvisoringContract>(r => r.AdvisoringRequestId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ContractStatus)
            .WithOne()
            .HasForeignKey<AdvisoringContract>(r => r.ContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ServiceType)
            .WithOne()
            .HasForeignKey<AdvisoringContract>(r => r.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ActorSecondaryRole>()
            .HasIndex(asr => new { asr.ActorId, asr.RoleId })
            .IsUnique();

        builder.Entity<ActorSecondaryRole>()
            .HasOne(asr => asr.Actor)
            .WithMany(a => a.ActorSecondaryRoles)
            .HasForeignKey(asr => asr.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ActorSecondaryRole>()
            .HasOne(asr => asr.Role)
            .WithMany(r => r.ActorSecondaryRoles)
            .HasForeignKey(asr => asr.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relaciones para AdvisoringContract
        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.StudentActor)
            .WithMany()
            .HasForeignKey(ac => ac.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.AdvisorActor)
            .WithMany()
            .HasForeignKey(ac => ac.AdvisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.ResearchGroupActor)
            .WithMany()
            .HasForeignKey(ac => ac.ResearchGroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.ResearchLineActor)
            .WithMany()
            .HasForeignKey(ac => ac.ResearchLineId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.ResearchAreaActor)
            .WithMany()
            .HasForeignKey(ac => ac.ResearchAreaId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.ServiceType)
            .WithMany()
            .HasForeignKey(ac => ac.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.ContractStatus)
            .WithMany()
            .HasForeignKey(ac => ac.ContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringContract>()
            .HasOne(ac => ac.AdvisoringRequest)
            .WithOne(ar => ar.AdvisoringContract)
            .HasForeignKey<AdvisoringContract>(ac => ac.AdvisoringRequestId);


        // Configurar relaciones para AdvisoringRequest
        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.RequesterActor)
            .WithMany()
            .HasForeignKey(ar => ar.RequesterActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.AdvisorActor)
            .WithMany()
            .HasForeignKey(ar => ar.AdvisorActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.UserActor)
            .WithMany()
            .HasForeignKey(ar => ar.UserActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.ResearchGroupActor)
            .WithMany()
            .HasForeignKey(ar => ar.ResearchGroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.ResearchLineActor)
            .WithMany()
            .HasForeignKey(ar => ar.ResearchLineId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.ResearchAreaActor)
            .WithMany()
            .HasForeignKey(ar => ar.ResearchAreaId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdvisoringRequest>()
            .HasOne(ar => ar.ServiceType)
            .WithMany()
            .HasForeignKey(ar => ar.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        //builder.Entity<AdvisoringContract>()
        //        .HasMany(e => e.DocumentCollections)
        //        .WithOne()
        //        .HasForeignKey(v => v.ContractId)
        //        .OnDelete(DeleteBehavior.Restrict);

        // Configurar relaciones para Thesis
        builder.Entity<Thesis>(entity =>
        {
            entity.HasOne(t => t.TesistaMembership)
                .WithMany(m => m.TesistaThesis)
                .HasForeignKey(t => t.TesistaMembershipId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.MainAdvisorMembership)
                .WithMany(m => m.MainAdvisorThesis)
                .HasForeignKey(t => t.MainAdvisorMembershipId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Secondary1AdvisorMembership)
                .WithMany(m => m.Secondary1AdvisorThesis)
                .HasForeignKey(t => t.Secondary1AdvisorMembershipId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Secondary2AdvisorMembership)
                .WithMany(m => m.Secondary2AdvisorThesis)
                .HasForeignKey(t => t.Secondary2AdvisorMembershipId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: Advance
        // -----------------------------
        builder.Entity<Advance>(entity =>
        {
            // Relación Advance - Project (muchos a 1)
            entity.HasOne(a => a.Project)
                .WithMany(p => p.Advances)
                .HasForeignKey(a => a.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Advance - DeliverableDocumentCollection (1 a 0..1, unidireccional)
            entity.HasOne(a => a.DeliverableDocumentCollection)
                .WithMany() // DocumentCollection no conoce a Advance
                .HasForeignKey(a => a.DeliverableDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Advance - ReportOfResearchGroupDocumentCollection (1 a 0..1, unidireccional)
            entity.HasOne(a => a.ReportOfResearchGroupDocumentCollection)
                .WithMany() // Sin navegación inversa en DocumentCollection
                .HasForeignKey(a => a.ReportOfResearchGroupDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: AdvanceEvaluation
        // -----------------------------
        builder.Entity<AdvanceEvaluation>(entity =>
        {
            // Relación AdvanceEvaluation - Advance (muchos a 1)
            entity.HasOne(ae => ae.Advance)
                .WithMany(a => a.AdvanceEvaluations)
                .HasForeignKey(ae => ae.AdvanceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación AdvanceEvaluation - Actor (evaluador)
            entity.HasOne(ae => ae.Actor)
                .WithMany(a => a.AdvanceEvaluations)
                .HasForeignKey(ae => ae.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación AdvanceEvaluation - Status (MasterDataValue)
            entity.HasOne(ae => ae.StatusEvaluation)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(ae => ae.StatusEvaluationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación AdvanceEvaluation - DocumentCollection (unidireccional)
            entity.HasOne(ae => ae.DocumentCollection)
                .WithMany() // DocumentCollection no conoce a AdvanceEvaluation
                .HasForeignKey(ae => ae.DocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: Funding
        // -----------------------------
        builder.Entity<Funding>(entity =>
        {
            // Relación CompetitiveFund - Project (muchos a 1)
            entity.HasOne(cf => cf.Project)
                .WithMany(p => p.Fundings)
                .HasForeignKey(cf => cf.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: DocumentCollection
        // -----------------------------
        builder.Entity<DocumentCollection>(entity =>
        {
            // Relación DocumentCollection - DocumentType (MasterDataValue) (unidireccional)
            entity.HasOne(dc => dc.DocumentType)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(dc => dc.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación DocumentCollection - UploadedByActor (Actor)
            entity.HasOne(dc => dc.UploadedByActor)
                .WithMany(a => a.DocumentCollections)
                .HasForeignKey(dc => dc.UploadedByActorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<DocumentVersion>(entity =>
        {
            entity.HasOne(dc => dc.DocumentCollection)
                .WithMany(a => a.DocumentVersions)
                .HasForeignKey(dc => dc.DocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: Project
        // -----------------------------
        builder.Entity<Project>(entity =>
        {
            // Relaciones con entidades Actor asignadas a llaves foráneas
            entity.HasOne(p => p.ResearchGroupProject)
                .WithMany(a => a.ResearchGroupProjects)
                .HasForeignKey(p => p.ResearchGroupProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ResearchAreaProject)
                .WithMany(a => a.ResearchAreaProjects)
                .HasForeignKey(p => p.ResearchAreaProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ResearchLineProject)
                .WithMany(a => a.ResearchLineProjects)
                .HasForeignKey(p => p.ResearchLineProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones con MasterDataValue para Method, Classification y Estado
            entity.HasOne(p => p.MethodProject)
                .WithMany()
                .HasForeignKey(p => p.MethodProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ClassificationProject)
                .WithMany()
                .HasForeignKey(p => p.ClassificationProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.StateProject)
                .WithMany()
                .HasForeignKey(p => p.StateProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.OdsObjective)
                .WithMany()
                .HasForeignKey(p => p.OdsObjectiveId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación para el autor del proyecto
            entity.HasOne(p => p.AuthorProject)
                .WithMany(a => a.AuthorProjects)
                .HasForeignKey(p => p.AuthorProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones con DocumentCollection (cada una es 1 a 0..1, sin navegación inversa)
            entity.HasOne(p => p.ResolutionDocumentCollection)
                .WithMany()
                .HasForeignKey(p => p.ResolutionDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.PlanDocumentCollection)
                .WithMany()
                .HasForeignKey(p => p.PlanDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ReportAdvisorDocumentCollection)
                .WithMany()
                .HasForeignKey(p => p.ReportAdvisorDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ReportOfResearchGroupDocumentCollection)
                .WithMany()
                .HasForeignKey(p => p.ReportOfResearchGroupDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ReportOfDegreesAndTittlesCommittee)
                .WithMany()
                .HasForeignKey(p => p.ReportOfDegreesAndTittlesCommitteeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Tabla Intermedia: ProjectActor
        // -----------------------------
        builder.Entity<ProjectActor>(entity =>
        {
            // Relación ProjectActor - Project
            entity.HasOne(pa => pa.Project)
                .WithMany(p => p.ProjectActors)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ProjectActor - Actor
            entity.HasOne(pa => pa.Actor)
                .WithMany(a => a.ProjectActors)
                .HasForeignKey(pa => pa.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con MasterDataValue para el tipo de autor
            entity.HasOne(pa => pa.AuthorType)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(pa => pa.AuthorTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Tabla Intermedia: ProjectEvaluation
        // -----------------------------
        builder.Entity<ProjectEvaluation>(entity =>
        {
            // Relación ProjectEvaluation - Project
            entity.HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEvaluations)
                .HasForeignKey(pe => pe.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ProjectEvaluation - Actor (evaluador)
            entity.HasOne(pe => pe.Actor)
                .WithMany(a => a.ProjectEvaluations)
                .HasForeignKey(pe => pe.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con MasterDataValue para el estado de evaluación
            entity.HasOne(pe => pe.StatusEvaluation)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(pe => pe.StatusEvaluationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ProjectEvaluation - DocumentCollection (unidireccional)
            entity.HasOne(pe => pe.DocumentCollection)
                .WithMany()
                .HasForeignKey(pe => pe.DocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Entidad: ScientificProduction
        // -----------------------------
        builder.Entity<ScientificProduction>(entity =>
        {
            // Relación ScientificProduction - Project
            entity.HasOne(sp => sp.Project)
                .WithMany(p => p.ScientificProductions)
                .HasForeignKey(sp => sp.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con MasterDataValue para ProductionType
            entity.HasOne(sp => sp.ProductionType)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(sp => sp.ProductionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ScientificProduction - DeliverableDocumentCollection (unidireccional)
            entity.HasOne(sp => sp.DeliverableDocumentCollection)
                .WithMany()
                .HasForeignKey(sp => sp.DeliverableDocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -----------------------------
        // Tabla Intermedia: ScientificProductionEvaluation
        // -----------------------------
        builder.Entity<ScientificProductionEvaluation>(entity =>
        {
            // Relación ScientificProductionEvaluation - ScientificProduction
            entity.HasOne(spe => spe.ScientificProduction)
                .WithMany(sp => sp.ScientificProductionEvaluations)
                .HasForeignKey(spe => spe.ScientificProductionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ScientificProductionEvaluation - Actor
            entity.HasOne(spe => spe.Actor)
                .WithMany(a => a.ScientificProductionEvaluations)
                .HasForeignKey(spe => spe.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con MasterDataValue para el estado de evaluación
            entity.HasOne(spe => spe.StatusEvaluation)
                .WithMany() // Sin navegación inversa
                .HasForeignKey(spe => spe.StatusEvaluationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ScientificProductionEvaluation - DocumentCollection (unidireccional)
            entity.HasOne(spe => spe.DocumentCollection)
                .WithMany()
                .HasForeignKey(spe => spe.DocumentCollectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }
}