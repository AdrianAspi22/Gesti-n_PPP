using System.Text.Json;
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Options;

namespace GestionAsesoria.Operator.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}