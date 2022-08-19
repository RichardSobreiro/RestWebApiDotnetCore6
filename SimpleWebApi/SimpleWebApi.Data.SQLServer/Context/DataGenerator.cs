using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Domain.Entities;

namespace SimpleWebApi.Data.SQLServer.Context
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SQLServerContext(
                serviceProvider.GetRequiredService<DbContextOptions<SQLServerContext>>()))
            {
                // Look for any board games.
                if (context.Accounts.Any())
                {
                    return;   // Data was already seeded
                }

                context.Accounts.AddRange(
                    new Account
                    {
                        DocumentNumber = "123456789",
                        AccountType = "FP",
                        Password = "7c4a8d09ca3762af61e59520943dc26494f8941b",
                        Id = Guid.Parse("c7dce21b-d207-4869-bf5f-08eb138bb919"),
                        DateCreated = DateTime.Now,
                        IsDeleted = false,
                        DateUpdated = null
                    });

                context.SaveChanges();
            }
        }
    }
}
