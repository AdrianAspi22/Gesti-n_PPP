using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionAsesoria.Operator.Domain.Entities.Identity
{
    public class AcademicUser : IdentityUser<string>, IAuditableEntity<string>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int? ChargeId { get; set; }
        public int? ActorId { get; set; }
        //public string? UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Column(TypeName = "text")]
        public string? ProfilePictureDataUrl { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        //public virtual AcademicUser User { get; set; }
        public virtual Actor Actor { get; set; }
        //public virtual ICollection<AcademicUser> Subordinates { get; set; }

    }
}