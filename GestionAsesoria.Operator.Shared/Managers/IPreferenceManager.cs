using GestionAsesoria.Operator.Shared.Settings;

using System.Threading.Tasks;
using GestionAsesoria.Operator.Shared.Wrapper;

namespace GestionAsesoria.Operator.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}