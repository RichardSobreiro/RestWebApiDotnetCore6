using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleWebApi.Domain.Base;
using SimpleWebApi.Domain.Entities;

namespace SimpleWebApi.Data.SQLServer.Extensions
{
    public static class ModelBuilderExtension
    {

        public static ModelBuilder ApplyGlobalConfigurations(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entity.Id):
                            property.IsKey();
                            break;
                        case nameof(Entity.DateUpdated):
                            property.IsNullable = true;
                            break;
                        case nameof(Entity.DateCreated):
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        case nameof(Entity.IsDeleted):
                            property.IsNullable = false;
                            property.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder;
        }

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasData(
                    new Account { 
                        DocumentNumber = "123456789", 
                        AccountType = "FP", 
                        Password = "7c4a8d09ca3762af61e59520943dc26494f8941b",
                        Id = Guid.Parse("c7dce21b-d207-4869-bf5f-08eb138bb919"), 
                        DateCreated = DateTime.Now, 
                        IsDeleted = false, DateUpdated = null 
                    }
                );


            return builder;
        }
    }
}
