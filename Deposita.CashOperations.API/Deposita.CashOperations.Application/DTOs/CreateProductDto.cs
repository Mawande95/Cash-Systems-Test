

using System.ComponentModel.DataAnnotations;

namespace Deposita.CashOperations.Application.DTOs
{
    public class CreateProductDto
    {
        public required decimal Amount { get; set; }
        [MaxLength(255)]
        public required string Description { get; set; }
        [MaxLength(50)]
        public required string TransactionType { get; set; }
    }
}
