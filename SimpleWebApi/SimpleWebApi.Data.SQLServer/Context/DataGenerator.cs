using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Domain.Entities;
using System.Security.Cryptography;

namespace SimpleWebApi.Data.SQLServer.Context
{
    public class DataGenerator
    {
        private static Random random = new Random();

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
                const string chars = "0123456789";
                context.Accounts.AddRange(
                        Enumerable
                            .Range(1, 100)
                            .Select(_ => new Account
                            {
                                DocumentNumber = new string(Enumerable.Repeat(chars, 9)
                                    .Select(s => s[random.Next(s.Length)]).ToArray()),
                                AccountType = "FP",
                                Password = SHA1.Create().ToString(),
                                Id = Guid.NewGuid(),
                                DateCreated = DateTime.Now,
                                IsDeleted = false,
                                DateUpdated = null
                            })
                            .ToArray()
                    );

                context.SaveChanges();
            }
        }
    }
}
