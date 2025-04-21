using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Permite crear una cita, incluyendo detalles sobre la fecha, ubicación y estado actual.")]
    public class Appointment : AuditableEntity<int>
    {
        public Appointment()
        {
            AdvisingSessions = new HashSet<AdvisingSession>();
        }

        [Comment("Fecha de la cita.")]
        public DateTime Date { get; set; }

        [Comment("Ubicación de la cita.")]
        public string Location { get; set; }

        [Comment("Identificador del estado actual de la cita.")]
        public int CurrentAppointmentStatusId { get; set; }

        [Comment("Estado actual de la cita.")]
        public virtual CurrentAppointmentStatus CurrentAppointmentStatus { get; set; }

        [Comment("Colección de sesiones de asesoría asociadas a la cita.")]
        public ICollection<AdvisingSession> AdvisingSessions { get; set; }
    }
}
