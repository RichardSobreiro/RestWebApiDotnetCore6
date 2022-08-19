using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebApi.Domain.Entities;

namespace SimpleWebApi.Data.SQLServer.Mappings
{
    internal class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.DocumentNumber).HasMaxLength(20).IsRequired();
            builder.Property(a => a.AccountType).HasMaxLength(5).IsRequired();
            builder.Property(a => a.Password).IsRequired().HasDefaultValue("");
        }
    }
}
