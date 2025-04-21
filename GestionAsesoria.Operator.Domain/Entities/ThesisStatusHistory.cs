using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa el historial de estado de una tesis, incluyendo detalles sobre el estado, fecha de emisión y archivo asociado.")]
    public class ThesisStatusHistory : AuditableEntity<int>
    {

        [Comment("Fecha en la que se emite el estado.")]
        public DateTime IssueDate { get; set; }

        [Comment("Archivo asociado al estado de la tesis.")]
        public string File { get; set; }

        [Comment("Identificador de la tesis asociada.")]
        public int ThesisId { get; set; }

        public int ThesisStatusId { get; set; }

        [Comment("Relación al Estado de la Tesis (PLAN PRESENTADO, PLAN OBSERVADO, TESIS EN EJECUCIÓN, TESIS ANULADA, EN REVISIÓN POR COMISIÓN, OBSERVADA POR COMISIÓN, REVISIÓN POR LOS JURADOS, POR SUSTENTAR, APROBADA, DESAPROBADA). Es una propiedad de navegación")]
        public virtual MasterDataValue ThesisStatus { get; set; }

        [Comment("Tesis asociada al historial de estado.")]
        public virtual Thesis Thesis { get; set; }
    }
}
