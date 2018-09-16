namespace Crosscutting.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message): base($"Validation error: {message}")
        {
        }
    }
}