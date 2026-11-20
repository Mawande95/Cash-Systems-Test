
namespace Deposita.CashOperations.Application.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public DateTime DateCreated { get; set; }
        public  decimal Amount { get; set; }
        public  string Description { get; set; }
        public  string TransactionType { get; set; }
    }
}
