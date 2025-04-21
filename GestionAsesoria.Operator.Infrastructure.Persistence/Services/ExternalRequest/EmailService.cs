using GestionAsesoria.Operator.Application.DTOs.Mail.Request;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Infrastructure.Persistence.Configurations;
using GestionAsesoria.Operator.Shared.Static;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Services.ExternalRequest
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSettings _emailSettings;

        public EmailService(ILogger<EmailService> logger, IOptions<EmailSettings> emailSettings)
        {
            // Cargar la configuración del correo desde appsettings.json
            _emailSettings = emailSettings.Value;
            _smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };
            _logger = logger;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(mailRequest.From ?? _emailSettings.FromEmail),
                Subject = mailRequest.Subject,
                Body = mailRequest.Body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(mailRequest.To);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation(ReplyMessage.MESSAGE_EMAIL_CORRECT);
            }
            catch (Exception)
            {
                _logger.LogError(ReplyMessage.MESSAGE_EMAIL_ERROR);
            }
        }
    }
}
