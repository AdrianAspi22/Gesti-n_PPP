using System;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        string CurrentTime { get; }

    }
}