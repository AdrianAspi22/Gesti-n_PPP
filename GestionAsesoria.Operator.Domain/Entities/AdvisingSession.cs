using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una sesión de asesoría, que incluye tareas y notas relacionadas con una cita específica.")]
    public class AdvisingSession : AuditableEntity<int>
    {
        public AdvisingSession()
        {
            AdvisoringTasks = new HashSet<AdvisoringTask>();
        }

        [Comment("Nota adicional sobre la sesión de asesoría, el \"?\" nos dice que es opcional llenar ese campo")]
        public string? Note { get; set; }

        //Mapeo para llaves Foraneas
        public int AdvisoringContractId { get; set; }
        public int AppointmentId { get; set; }

        [Comment("Cita asociada a la sesión de asesoría.")]
        public virtual Appointment Appointment { get; set; }
        [Comment("Contrato de asesoría asociado a la sesión.")]
        public virtual AdvisoringContract AdvisoringContract { get; set; }
        [Comment("Colección de tareas de asesoría asociadas a la sesión.")]
        public virtual ICollection<AdvisoringTask> AdvisoringTasks { get; set; }
    }
}
