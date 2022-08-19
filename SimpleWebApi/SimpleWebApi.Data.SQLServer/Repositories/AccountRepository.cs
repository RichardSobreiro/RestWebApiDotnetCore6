using SimpleWebApi.Data.SQLServer.Context;
using SimpleWebApi.Domain.Entities;
using SimpleWebApi.Domain.Repository;

namespace SimpleWebApi.Data.SQLServer.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(SQLServerContext sQLServerContext) : base(sQLServerContext)
        {
        }
        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await QueryAsync(x => !x.IsDeleted);
        }
    }
}
