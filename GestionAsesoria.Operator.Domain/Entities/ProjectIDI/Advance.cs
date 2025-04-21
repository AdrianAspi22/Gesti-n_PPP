using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class Advance : AuditableEntity<int>
    {
        public Advance()
        {
            AdvanceEvaluations = new HashSet<AdvanceEvaluation>();
        }

        // BÁSICO

        [Comment("Fecha del avance del proyecto.")]
        public DateTime AdvanceDate { get; set; }

        [Comment("Descripción del avance.")]
        public string Description { get; set; }


        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador del proyecto al que pertenece este avance.")]
        public int ProjectId { get; set; }
        [Comment("Entidad Project asociada a este avance.")]
        public virtual Project Project { get; set; }

        [Comment("Identificador de la colección de documentos entregables (DocumentCollection).")]
        public int DeliverableDocumentCollectionId { get; set; }
        [Comment("Entidad DocumentCollection que representa la colección de documentos entregables.")]
        public virtual DocumentCollection DeliverableDocumentCollection { get; set; }

        [Comment("Identificador de la colección de documentos del informe del grupo de investigación (DocumentCollection).")]
        public int? ReportOfResearchGroupDocumentCollectionId { get; set; }
        [Comment("Entidad DocumentCollection que representa la colección de documentos del informe del grupo de investigación.")]
        public virtual DocumentCollection ReportOfResearchGroupDocumentCollection { get; set; }


        //COLECCTIONS

        [Comment("Colección de evaluaciones asociadas al avance mediante la tabla intermedia AdvanceEvaluation.")]
        public virtual ICollection<AdvanceEvaluation> AdvanceEvaluations { get; set; }
    }
}
