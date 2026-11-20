using Deposita.CashOperations.Application.Interfaces.Repositories;
using Deposita.CashOperations.Domain.Entities;
using Deposita.CashOperations.Persistence.Context;


namespace Deposita.CashOperations.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}
