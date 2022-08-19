namespace SimpleWebApi.Domain.Base.Exceptions
{
    public class FieldValidationException : Exception
    {
        public FieldValidationException(string message) : base(message) { }

        public int StatusCode = 422; // Unprocessable Entity
    }
}
