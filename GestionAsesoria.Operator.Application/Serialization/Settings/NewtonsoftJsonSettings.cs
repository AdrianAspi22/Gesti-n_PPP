
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace GestionAsesoria.Operator.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}