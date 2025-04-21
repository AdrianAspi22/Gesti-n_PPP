using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa los estados de una cita, el cual incluye el nombre del estado, la fecha y comentarios adicionales.")]
    public class AppointmentStatus : AuditableEntity<int>
    {
        [Comment("Nombre del estado de la cita.")]
        public string StatusName { get; set; }

        [Comment("Fecha en la que se asigna el estado.")]
        public DateTime StatusDate { get; set; }

        [Comment("Identificador del estado.")]
        public int StateId { get; set; }

        [Comment("Comentario adicional sobre el estado de la cita.")]
        public string Comment { get; set; }
    }
}
