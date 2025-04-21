using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class DocumentCollection : AuditableEntity<int>
    {
        public DocumentCollection(){
            DocumentVersions = new HashSet<DocumentVersion>();
        }
        // BÁSICO
        public string Title { get; set; }
        public string Description { get; set; }
        [Comment("Ruta del archivo en el sistema.")]
        public string FilePath { get; set; }
        
        [Comment("Tamaño de archivo).")]
        public long FileSize { get; set; }


        [Comment("Fecha en la que se subió el documento.")]
        public DateTime UploadDate { get; set; }

        [Comment("URL pública del documento en línea.")]
        public string OnlineUrl { get; set; }

        // LLAVES FORÁNEAS Y PROPIEDAD DE NAVEGACIÓN

        [Comment("Identificador del tipo de documento (MasterDataValue).")]
        public int DocumentTypeId { get; set; }
        [Comment("Entidad MasterDataValue que representa el tipo de documento.")]
        public virtual MasterDataValue DocumentType { get; set; }

        [Comment("Identificador del usuario que subió el documento (Actor).")]
        public int UploadedByActorId { get; set; }
        [Comment("Entidad Actor que representa al usuario que subió el documento.")]
        public virtual Actor UploadedByActor { get; set; }

        //COLECCIONES

        public virtual ICollection<DocumentVersion> DocumentVersions { get; set; }
    }

}
