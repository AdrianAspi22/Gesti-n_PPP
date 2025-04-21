using GestionAsesoria.Operator.Domain.Auditable;
using GestionAsesoria.Operator.Domain.Entities.Identity;
using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa a un actor en el sistema, el cual está involucrada en contratos de asesoría.")]
    public class Actor : AuditableEntity<int>
    {
        public Actor()
        {
            AdvisoringContracts = new HashSet<AdvisoringContract>();
            Memberships = new HashSet<Membership>();
            Calendars = new HashSet<Calendar>();
            Users = new HashSet<AcademicUser>();
            ActorSecondaryRoles = new HashSet<ActorSecondaryRole>();
            DocumentCollections = new HashSet<DocumentCollection>();
            ResearchGroupProjects = new HashSet<Project>();
            ResearchLineProjects = new HashSet<Project>();
            ResearchAreaProjects = new HashSet<Project>();
            AuthorProjects = new HashSet<Project>();
            ProjectActors = new HashSet<ProjectActor>();
            ProjectEvaluations = new HashSet<ProjectEvaluation>();
            AdvanceEvaluations = new HashSet<AdvanceEvaluation>();
            ScientificProductionEvaluations = new HashSet<ScientificProductionEvaluation>();

        }
        public string Code { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Comment("Dependerá del contexto del Actor, si es persona Natural será el Nombre de la persona, si es persona jurídica será la Razón Social, si es entidad interna, llevará el nombre de la entidad como por ejemplo para actor Grupo de Investigación, Grupo de Investigación de Ingeniería de Software")]
        public string FirstName { get; set; }
        [Comment("Dependerá del contexto del Actor, si es persona Natural será el Apellido de la persona, si es persona jurídica será la Nombre Comercial, si es entidad interna, llevará el Acrónimo de la entidad como por ejemplo para actor Grupo de Investigación, su acrónimo será GINSOFT")]
        public string SecondName { get; set; }
        public string ThirdName { get; set; }

        [Comment("Es un clasificador para seleccionar si es persona Natural será su género ejemplo: Masculino y Femenino, si es persona Jurídica se seleccionará: SAC, EIRL, Etc")]
        public string ClassifyActor { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActived { get; set; }


        //Otros Datos
        public string? CodeORCID { get; set; }
        public string? CTIVitaeCONCYTEC { get; set; }
        [Comment("Mayor Grado Académico")]
        public string? HigherDegree { get; set; }


        [Comment("Configuraciones del calendario del actor, se guardará mediante una cadena de texto y estará separado por comas (por ejemplo 12, 14, 15), esta máscara vendra desde el front. Donde: 12--> IdCalendarioTesis   14--> IdCalendarioPPP   15--> IdCalendarioTutoría")]
        public string CalendarSettings { get; set; }

        //Mapeo para llaves Foraneas
        public int? IdentificationTypeId { get; set; }
        public int ActorTypeId { get; set; }
        public int? ParentId { get; set; }
        public int? MainRoleId { get; set; }

        [Comment("Relación al tipo de actor (natural, jurídica, o entidad interna; una entidad interna se dice a los grupos, areas, lineas de investigación, laboratorios, facultades, escuelas profesionales, instituto de investigación, etc). Es una propiedad de navegación")]
        public virtual ActorType ActorType { get; set; }
        public virtual Actor Parent { get; set; }

        [Comment("Relación con Role y será el Rol padre que cuente el Actor,por ejemplo Universidad, Facultad, Escuela Profesional, Grupo de investigación, Docente, Estudiante, Decano, Director de Investigacipon, Coordinador de Grupo, Etc.")]
        public virtual Role MainRole { get; set; }

        [Comment("Relación con MasterDataValue para el tipo de Identificación.")]
        public virtual MasterDataValue IdentificationType { get; set; }

        [Comment("Colección de membresías asociadas al actor.")]
        public virtual ICollection<Membership> Memberships { get; set; }

        [Comment("Colección de contratos de asesoría asociados al actor.")]
        public virtual ICollection<AdvisoringContract> AdvisoringContracts { get; set; }

        [Comment("Servirá para la asignación de 2 a más Roles a un determinado Actor")]
        public virtual ICollection<ActorSecondaryRole> ActorSecondaryRoles { get; set; }


        [Comment("Colección de calendarios asociados al actor. El actor de tipo Asesor, dispondrá de varios tipos de calendario (tesis, ppp, tutoría, etc)")]
        public virtual ICollection<Calendar> Calendars { get; set; }

        public virtual ICollection<AcademicUser> Users { get; set; }

        public virtual ICollection<DocumentCollection> DocumentCollections { get; set; }

        public virtual ICollection<Project> ResearchGroupProjects { get; set; }
        public virtual ICollection<Project> ResearchLineProjects { get; set; }
        public virtual ICollection<Project> ResearchAreaProjects { get; set; }
        public virtual ICollection<Project> AuthorProjects { get; set; }

        [Comment("Colección de participaciones en proyectos mediante la tabla intermedia ProjectActor.")]
        public virtual ICollection<ProjectActor> ProjectActors { get; set; }

        [Comment("Colección de evaluaciones realizadas por el actor mediante la tabla intermedia ProjectEvaluation.")]
        public virtual ICollection<ProjectEvaluation> ProjectEvaluations { get; set; }

        [Comment("Colección de evaluaciones de avances realizadas por el actor mediante la tabla intermedia AdvanceEvaluation.")]
        public virtual ICollection<AdvanceEvaluation> AdvanceEvaluations { get; set; }

        [Comment("Colección de evaluaciones de producciones científicas realizadas por el actor mediante la tabla intermedia ProductionScientificEvaluation.")]
        public virtual ICollection<ScientificProductionEvaluation> ScientificProductionEvaluations { get; set; }

    }
}
