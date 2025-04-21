using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities.Parameters
{
    public partial class BusinessSetting
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BusinessSettingParameter> BusinessSettingParameters { get; set; } = new List<BusinessSettingParameter>();
    }
}
