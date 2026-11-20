

using Deposita.CashOperations.Application.Interfaces.Services;
using Deposita.CashOperations.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deposita.CashOperations.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(
                cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));


            //Services
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
