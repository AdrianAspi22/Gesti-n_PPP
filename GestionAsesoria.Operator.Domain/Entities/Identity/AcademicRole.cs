using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities.Identity
{
    public class AcademicRole : IdentityRole, IAuditableEntity<string>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual ICollection<AcademicRoleClaim> RoleClaims { get; set; }

        public AcademicRole() : base()
        {
            RoleClaims = new HashSet<AcademicRoleClaim>();
        }

        public AcademicRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<AcademicRoleClaim>();
            Description = roleDescription;
        }
    }
}