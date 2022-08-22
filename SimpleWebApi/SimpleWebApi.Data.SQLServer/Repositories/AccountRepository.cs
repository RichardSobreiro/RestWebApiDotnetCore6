using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data.SQLServer.Context;
using SimpleWebApi.Domain.Base.Pagination;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Repository;
using SimpleWebApi.Services.Extensions;
using System.Linq.Expressions;

namespace SimpleWebApi.Data.SQLServer.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly SQLServerContext sQLServerContext;
        public AccountRepository(SQLServerContext sQLServerContext) : base(sQLServerContext)
        {
            this.sQLServerContext = sQLServerContext;
        }
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await QueryAsync(x => !x.IsDeleted);
        }
        public async Task<PagedModel<Account>> QueryPaginatedAsync(
           int limit, int page, CancellationToken cancellationToken)
        {
            try
            {
                var results = await sQLServerContext.Accounts
                    .AsNoTracking()
                    .OrderBy(a => a.DateCreated)
                    .PaginateAsync(page, limit, cancellationToken);
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
