using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class Project : AuditableEntity<int>
    {
        public Project()
        {
            Advances = new HashSet<Advance>();
            ProjectActors = new HashSet<ProjectActor>();
            ProjectEvaluations = new HashSet<ProjectEvaluation>();
            ScientificProductions = new HashSet<ScientificProduction>();
            Fundings = new HashSet<Funding>();
        }

        //BÁSICO

        [Comment("Título o nombre del proyecto.")]
        public string Title { get; set; }

        [Comment("Fecha de inicio del proyecto.")]
        public DateTime StartDate { get; set; }

        [Comment("Fecha de finalización del proyecto.")]
        public DateTime? EndDate { get; set; }

        [Comment("Lugar donde se ejecuta el proyecto.")]
        public string ExecutionPlace { get; set; }

        [Comment("Estado activo del proyecto (true = activo; false = inactivo).")]
        public bool IsActived { get; set; }

        
        //Llaves Foráneas y Propiedades de Navegación

        [Comment("Identificador del grupo de investigación asociado al proyecto (Actor).")]
        public int ResearchGroupProjectId { get; set; }
        [Comment("Entidad Actor que representa el grupo de investigación asociado.")]
        public virtual Actor ResearchGroupProject { get; set; }

        [Comment("Identificador del área de investigación vinculada (Actor).")]
        public int ResearchAreaProjectId { get; set; }
        [Comment("Entidad Actor que representa el área de investigación vinculada.")]
        public virtual Actor ResearchAreaProject { get; set; }

        [Comment("Identificador de la línea de investigación correspondiente (Actor).")]
        public int ResearchLineProjectId { get; set; }
        [Comment("Entidad Actor que representa la línea de investigación correspondiente.")]
        public virtual Actor ResearchLineProject { get; set; }

        [Comment("Identificador del método aplicado en la ejecución del proyecto (MasterDataValue).")]
        public int MethodProjectId { get; set; }
        public virtual MasterDataValue MethodProject { get; set; }

        [Comment("Identificador del obejtivo ODS para el proyecto (MasterDataValue).")]
        public int OdsObjectiveId { get; set; }
        public virtual MasterDataValue OdsObjective { get; set; }

        [Comment("Identificador de la clasificación asignada al proyecto (MasterDataValue).")]
        public int ClassificationProjectId { get; set; }
        public virtual MasterDataValue ClassificationProject { get; set; }

        [Comment("Identificador del estado asignado al proyecto (MasterDataValue).")]
        public int StateProjectId { get; set; }
        public virtual MasterDataValue StateProject { get; set; }

        [Comment("Identificador del autor del proyecto (Actor).")]
        public int AuthorProjectId { get; set; }
        public virtual Actor AuthorProject { get; set; }

        [Comment("Identificador de la colección de documentos de resolución (DocumentCollection).")]
        public int? ResolutionDocumentCollectionId { get; set; }
        public virtual DocumentCollection ResolutionDocumentCollection { get; set; }

        [Comment("Identificador de la colección de documentos del plan (DocumentCollection).")]
        public int? PlanDocumentCollectionId { get; set; }
        public virtual DocumentCollection PlanDocumentCollection { get; set; }

        [Comment("Identificador de la colección de documentos del dictamen del asesor (DocumentCollection).")]
        public int? ReportAdvisorDocumentCollectionId { get; set; }
        public virtual DocumentCollection ReportAdvisorDocumentCollection { get; set; }

        [Comment("Identificador de la colección de documentos del dictamen o acuerdo del grupo de investigación (DocumentCollection).")]
        public int? ReportOfResearchGroupDocumentCollectionId { get; set; }
        public virtual DocumentCollection ReportOfResearchGroupDocumentCollection { get; set; }

        [Comment("Identificador de la colección de documentos del acuerdo de la comisión de grados y títulos")]
        public int? ReportOfDegreesAndTittlesCommitteeId { get; set; }
        public virtual DocumentCollection ReportOfDegreesAndTittlesCommittee { get; set; }

        //COLECCIONES

        [Comment("Colección de avances asociados al proyecto.")]
        public virtual ICollection<Advance> Advances { get; set; }

        [Comment("Colección de participantes (actores) asociados al proyecto.")]
        public virtual ICollection<ProjectActor> ProjectActors { get; set; }

        [Comment("Colección de evaluaciones del proyecto.")]
        public virtual ICollection<ProjectEvaluation> ProjectEvaluations { get; set; }

        [Comment("Colección de producciones científicas asociadas al proyecto.")]
        public virtual ICollection<ScientificProduction> ScientificProductions { get; set; }

        [Comment("Colección de fondos concursables asociados al proyecto.")]
        public virtual ICollection<Funding> Fundings { get; set; }
    }
}
