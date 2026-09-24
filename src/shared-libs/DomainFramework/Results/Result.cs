using DomainFramework.Errors;

namespace DomainFramework.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        private Result()
        {
            IsSuccess = true;
            Error = null!;
        }

        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success()
        {
            return new Result();
        }

        public static Result Failure(Error error)
        {
            return new Result(error);
        }
    }


    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public T Value { get; }
        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null!;
        }
        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;
            Value = default!;
        }
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}
