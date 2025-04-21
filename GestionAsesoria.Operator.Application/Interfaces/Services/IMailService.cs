using GestionAsesoria.Operator.Application.DTOs.Mail.Request;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}