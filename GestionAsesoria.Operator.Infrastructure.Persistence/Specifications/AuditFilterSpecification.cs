using dev_peru.Application.Specifications.Base;
using GestionAsesoria.Operator.Domain.Entities.Audit;

namespace GestionAsesoria.Operator.Infrastructure.Specifications
{
    public class AuditFilterSpecification : AsesorySpecification<Audit>
    {
        public AuditFilterSpecification(string userId, string searchString, bool searchInOldValues, bool searchInNewValues)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => (p.TableName!.Contains(searchString) || searchInOldValues && p.OldValues!.Contains(searchString) || searchInNewValues && p.NewValues!.Contains(searchString)) && p.UserId == userId;
            }
            else
            {
                Criteria = p => p.UserId == userId;
            }
        }
    }
}