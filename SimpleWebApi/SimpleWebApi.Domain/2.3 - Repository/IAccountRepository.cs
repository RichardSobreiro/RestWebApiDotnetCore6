using SimpleWebApi.Domain.Base.Pagination;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Interfaces;

namespace SimpleWebApi.Domain.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<PagedModel<Account>> QueryPaginatedAsync(
           int limit, int page, CancellationToken cancellationToken);
    }
}
