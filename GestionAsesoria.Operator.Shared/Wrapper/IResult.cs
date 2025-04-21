using FluentValidation.Results;

namespace GestionAsesoria.Operator.Shared.Wrapper
{
    public interface IResult
    {
        List<string> Messages { get; set; }
        bool Succeeded { get; set; }
        IEnumerable<ValidationFailure>? Errors { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}