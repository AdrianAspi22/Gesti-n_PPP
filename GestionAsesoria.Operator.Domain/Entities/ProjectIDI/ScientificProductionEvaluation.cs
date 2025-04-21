using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class ScientificProductionEvaluation : AuditableEntity<int>
    {
        // BÁSICO

        [Comment("Fecha en la que se realizó la evaluación.")]
        public DateTime EvaluationDate { get; set; }

        [Comment("Comentarios realizados por el evaluador.")]
        public string Comments { get; set; }

        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador de la producción científica evaluada.")]
        public int ScientificProductionId { get; set; }
        [Comment("Entidad ScientificProduction que representa la producción científica evaluada.")]
        public virtual ScientificProduction ScientificProduction { get; set; }

        [Comment("Identificador del evaluador (Actor).")]
        public int ActorId { get; set; }
        [Comment("Entidad Actor que representa al evaluador.")]
        public virtual Actor Actor { get; set; }

        [Comment("Identificador del estado de la evaluación (MasterDataValue).")]
        public int StatusEvaluationId { get; set; }
        [Comment("Entidad MasterDataValue que representa el estado de la evaluación.")]
        public virtual MasterDataValue StatusEvaluation { get; set; }

        [Comment("Identificador de la colección de documentos asociados a la evaluación (DocumentCollection).")]
        public int DocumentCollectionId { get; set; }
        [Comment("Entidad DocumentCollection que representa los documentos asociados a la evaluación.")]
        public virtual DocumentCollection DocumentCollection { get; set; }

    }
}
