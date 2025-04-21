using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.AspNetCore.Identity;
using System;

namespace GestionAsesoria.Operator.Domain.Entities.Identity
{
    public class AcademicRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? Group { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual AcademicRole Role { get; set; }
        public AcademicRoleClaim() : base()
        {
        }

        public AcademicRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}