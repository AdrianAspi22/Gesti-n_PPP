using GestionAsesoria.Operator.Application.Interfaces.Services;
using System;

namespace GestionAsesoria.Operator.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
        public string CurrentTime => DateTime.UtcNow.AddHours(-5).ToString("HH:mm:ss");
    }
}