using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa los valores asociados a los datos maestros en el sistema. Cada valor tiene un código, nombre y una descripción.")]
    public class MasterDataValue : AuditableEntity<int>
    {
        [Comment("Código único que identifica el valor dentro de los datos maestros. Este código es usado para referenciar el valor de manera única.")]
        public string Code { get; set; }

        [Comment("Nombre descriptivo del valor. Este nombre se utiliza para mostrar el valor en interfaces y reportes.")]
        public string Name { get; set; }

        [Comment("Valor asociado al dato maestro. Puede ser cualquier tipo de dato como una cadena, número o booleano dependiendo del tipo de dato maestro.")]
        public string Value { get; set; }

        [Comment("Fecha en que se creó el valor. Se utiliza para controlar cuándo fue insertado el valor en el sistema.")]
        public DateTime CreatedDate { get; set; }

        [Comment("Fecha de la última modificación del valor. Permite conocer cuándo se actualizó por última vez el registro.")]
        public DateTime LastModifiedDate { get; set; }

        [Comment("Identificador del dato maestro al que pertenece este valor. Es una clave foránea que refiere a la entidad MasterData.")]
        public int MasterDataId { get; set; }

        [Comment("Indica si el valor está activo o inactivo. Es útil para desactivar valores sin eliminarlos permanentemente.")]
        public bool IsActived { get; set; }

        [Comment("Descripción adicional sobre el valor. Se utiliza para proporcionar más información acerca del valor y su contexto.")]
        public string Description { get; set; }

        [Comment("Relación de navegación con la entidad MasterData. Permite acceder al dato maestro asociado a este valor.")]
        public virtual MasterData MasterData { get; set; }
    }
}
