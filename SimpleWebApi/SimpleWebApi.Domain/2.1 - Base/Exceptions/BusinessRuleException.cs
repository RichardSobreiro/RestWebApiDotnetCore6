namespace SimpleWebApi.Domain.Base.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }

        public int StatusCode = 409;
    }
}
