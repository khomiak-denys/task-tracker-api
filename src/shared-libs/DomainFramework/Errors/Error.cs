namespace DomainFramework.Errors
{
    public class Error(string message)
    {
        public string Message { get; } = message;
    }
}
