using GestionAsesoria.Operator.Domain.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class DocumentVersion : AuditableEntity<int>
    {
        public int DocumentCollectionId { get; set; }
        public string FilePath { get; set; }
        public string VersionNumber { get; set; }
        public string ChangeDescription { get; set; }
        public virtual DocumentCollection DocumentCollection { get; set; }
    }
}
