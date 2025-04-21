namespace GestionAsesoria.Operator.Domain.Entities.Parameters
{
    public partial class BusinessSettingParameter
    {
        public int Id { get; set; }

        public int BusinessSettingId { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public virtual BusinessSetting BusinessSetting { get; set; }
    }
}
