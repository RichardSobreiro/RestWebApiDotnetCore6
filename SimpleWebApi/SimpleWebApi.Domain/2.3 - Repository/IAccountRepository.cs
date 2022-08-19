using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Interfaces;

namespace SimpleWebApi.Domain.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetAllAsync();
    }
}
