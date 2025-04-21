using FluentValidation.Results;

namespace GestionAsesoria.Operator.Shared.Wrapper
{
    public class Result : IResult
    {
        public Result()
        {
        }


        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }
        public IEnumerable<ValidationFailure>? Errors { get; set; }

        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }
        public static IResult Fail(IEnumerable<ValidationFailure>? errors)
        {
            return new Result { Succeeded = false, Errors = errors ?? new List<ValidationFailure>() };
        }

        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Messages = new List<string> { message } };
        }



        public static IResult Fail(List<string> messages)
        {
            return new Result { Succeeded = false, Messages = messages };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResult> FailAsync(IEnumerable<ValidationFailure>? errors)
        {
            return Task.FromResult(Fail(errors));
        }

        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Messages = new List<string> { message } };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public Result()
        {
        }

        public Result(string message)
        {
            Succeeded = false;
            Messages = new List<string> { message };
        }
        public T Data { get; set; }

        public int TotalRecords { get; set; }

        public static new Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }

        public static new Result<T> Fail(string message)
        {
            return new Result<T> { Succeeded = false, Messages = new List<string> { message } };
        }

        public static Result<T> Fail(T data, string message)
        {
            return new Result<T> { Succeeded = false, Data = data, Messages = new List<string> { message } };
        }

        public static new Result<T> Fail(List<string> messages)
        {
            return new Result<T> { Succeeded = false, Messages = messages };
        }

        public static new Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static new Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static new Task<Result<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static Task<Result<T>> FailAsync(T data, string message)
        {
            return Task.FromResult(Fail(data, message));

        }
        public static new Result<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }

        public static new Result<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Messages = new List<string> { message } };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }



        public static Result<T> Success(T data, string message)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }

        public static Result<T> Success(T data, string message, int totalRecords)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = new List<string> { message }, TotalRecords = totalRecords };
        }



        public static new Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static new Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }



        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        public static Task<Result<T>> SuccessAsync(T data, string message, int totalRecords)
        {
            return Task.FromResult(Success(data, message, totalRecords));
        }
    }
}