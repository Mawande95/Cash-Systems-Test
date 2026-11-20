
using Deposita.CashOperations.Domain.Common;


namespace Deposita.CashOperations.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public required decimal Amount { get; set; }
        public required string Description { get; set; }
        public required string TransactionType { get; set; }
    }
}
