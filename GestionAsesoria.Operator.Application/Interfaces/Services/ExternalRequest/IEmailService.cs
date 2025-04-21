using GestionAsesoria.Operator.Application.DTOs.Mail.Request;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
