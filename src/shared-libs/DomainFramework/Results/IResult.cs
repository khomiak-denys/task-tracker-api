using DomainFramework.Errors;

namespace DomainFramework.Results
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public Error Error { get; }
    }


    public interface IResult<out T> : IResult
    {
        public T Value { get; }
    }
}
