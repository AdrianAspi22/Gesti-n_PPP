using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class Funding : AuditableEntity<int>
    {
        // BÁSICO

        [Comment("Monto asignado al financiamiento.")]
        public decimal Amount { get; set; }

        [Comment("Tipo de financiamiento Propio, FIF, etc).")]
        public string FundingType { get; set; }

        [Comment("Indica si el financiamiento participa en un fondo concursable.")]
        public bool IsCompetitiveFund { get; set; }

        [Comment("Nombre del fondo concursable (si aplica).")]
        public string FundingName { get; set; }

        [Comment("Nombre de la organización asociada al fondo (si aplica).")]
        public string Organization { get; set; }

        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador del proyecto asociado al financiamiento.")]
        public int ProjectId { get; set; }

        [Comment("Entidad Project que representa el proyecto asociado.")]
        public virtual Project Project { get; set; }
    }
}
