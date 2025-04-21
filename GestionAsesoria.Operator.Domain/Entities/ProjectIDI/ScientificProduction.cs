using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class ScientificProduction : AuditableEntity<int>
    {
        public ScientificProduction()
        {
            ScientificProductionEvaluations = new HashSet<ScientificProductionEvaluation>();
        }

        // BÁSICO

        [Comment("Título de la producción científica.")]
        public string Title { get; set; }

        [Comment("Fecha de publicación de la producción científica.")]
        public DateTime PublicationDate { get; set; }

        [Comment("Identificador DOI de la producción científica.")]
        public string? Doi { get; set; }

        [Comment("Código de la producción científica.")]
        public string Code { get; set; }

        [Comment("Fecha de creación del registro.")]
        public DateTime CreatedAt { get; set; }

        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador del proyecto asociado a la producción científica.")]
        public int ProjectId { get; set; }
        [Comment("Entidad Project que representa el proyecto asociado.")]
        public virtual Project Project { get; set; }

        [Comment("Identificador del tipo de producción científica (MasterDataValue).")]
        public int ProductionTypeId { get; set; }
        [Comment("Entidad MasterDataValue que representa el tipo de producción científica.")]
        public virtual MasterDataValue ProductionType { get; set; }

        [Comment("Identificador de la colección de documentos entregables (DocumentCollection).")]
        public int DeliverableDocumentCollectionId { get; set; }
        [Comment("Entidad DocumentCollection que representa la colección de documentos entregables.")]
        public virtual DocumentCollection DeliverableDocumentCollection { get; set; }


        //COLLECTIONS

        [Comment("Colección de evaluaciones asociadas a la producción científica mediante la tabla intermedia ProductionScientificEvaluation.")]
        public virtual ICollection<ScientificProductionEvaluation> ScientificProductionEvaluations { get; set; }
    }
}
