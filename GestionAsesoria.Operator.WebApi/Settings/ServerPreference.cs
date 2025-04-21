using System.Linq;
using GestionAsesoria.Operator.Shared.Constants.Localization;
using GestionAsesoria.Operator.Shared.Settings;

namespace GestionAsesoria.Operator.WebApi.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "es-ES";

        //TODO - add server preferences
    }
}