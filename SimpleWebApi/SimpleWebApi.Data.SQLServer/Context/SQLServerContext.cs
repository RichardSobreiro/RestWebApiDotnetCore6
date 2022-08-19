using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data.SQLServer.Extensions;
using SimpleWebApi.Data.SQLServer.Mappings;
using SimpleWebApi.Domain.Entities;

namespace SimpleWebApi.Data.SQLServer.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }

        #region DbSets
        internal DbSet<Account> Accounts { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());

            modelBuilder.ApplyGlobalConfigurations();
            base.OnModelCreating(modelBuilder);
        }
    }
}
