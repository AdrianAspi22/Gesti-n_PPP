using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa los datos maestros utilizados en el sistema, como códigos y tipos de datos relacionados.")]
    public class MasterData : AuditableEntity<int>
    {
        public MasterData()
        {
            MasterDataValues = new HashSet<MasterDataValue>();
        }

        [Comment("Código único que identifica el dato maestro en el sistema. Este código es utilizado para referenciar el dato de manera única.")]
        public string Code { get; set; }

        [Comment("Nombre descriptivo del dato maestro. Este nombre es usado para identificar el dato en interfaces y reportes.")]
        public string Name { get; set; }

        [Comment("Tipo del dato maestro, referenciado por su identificador (por ejemplo, tipo de datos como 'categoría', 'subcategoría', etc.).")]
        public int TypeId { get; set; }

        [Comment("Colección de valores asociados a este dato maestro. La relación es de uno a muchos, ya que un dato maestro puede tener múltiples valores asociados.")]
        public virtual ICollection<MasterDataValue> MasterDataValues { get; set; }
    }
}
