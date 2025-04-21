using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities.ProjectIDI
{
    public class ProjectActor : AuditableEntity<int>
    {
        // BÁSICO

        [Comment("Justificación para la participación del actor en el proyecto.")]
        public string Justification { get; set; }


        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador del proyecto asociado al actor.")]
        public int ProjectId { get; set; }
        [Comment("Entidad Project que representa el proyecto asociado.")]
        public virtual Project Project { get; set; }

        [Comment("Identificador del actor que participa en el proyecto (Actor).")]
        public int ActorId { get; set; }
        [Comment("Entidad Actor que representa al participante del proyecto.")]
        public virtual Actor Actor { get; set; }

        [Comment("Identificador del tipo de autor dentro del proyecto (MasterDataValue).")]
        public int AuthorTypeId { get; set; }
        [Comment("Entidad MasterDataValue que representa el tipo de autor del proyecto.")]
        public virtual MasterDataValue AuthorType { get; set; }

    }
}
