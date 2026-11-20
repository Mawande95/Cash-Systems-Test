using Deposita.CashOperations.Application.Interfaces.Repositories;
using Deposita.CashOperations.Persistence.Context;
using Deposita.CashOperations.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Deposita.CashOperations.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITransactionRepository,TransactionRepository>();

            return services;
        }
    }
}
