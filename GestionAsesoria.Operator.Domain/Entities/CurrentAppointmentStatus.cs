using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa el estado actual de una cita.")]
    public class CurrentAppointmentStatus : AuditableEntity<int>
    {
        [Comment("Nombre del estado actual de la cita.")]
        public string NameCurrentStatus { get; set; }

        public int AppointmentStatusId { get; set; }
        [Comment("Estado de la cita asociado al estado actual.")]
        public virtual AppointmentStatus AppointmentStatus { get; set; }
    }
}
