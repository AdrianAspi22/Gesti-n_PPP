namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IMessageService
    {
        // Método para obtener el mensaje basado en categoría, sección y clave
        string GetMessage(string category, string section, string key);

        // Método para obtener el mensaje con placeholders dinámicos
        string GetDynamicMessage(string category, string section, string key);
    }
}
