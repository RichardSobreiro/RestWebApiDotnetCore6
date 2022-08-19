using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Data.SQLServer.Repositories;
using SimpleWebApi.Domain.Repository;
using SimpleWebApi.Services.Interfaces;
using SimpleWebApi.Services.Services;

namespace SimpleWebApi.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();

            serviceCollection.AddScoped<IAccountService, AccountService>();
        }
    }
}