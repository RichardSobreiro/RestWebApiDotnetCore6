using SimpleWebApi.Domain.Base;

namespace SimpleWebApi.Domain.Entities
{
    public class Account : Entity
    {
        public string DocumentNumber { get; set; }
        public string AccountType { get; set; }
        public string Password { get; set; }

    }
}
