

using System.ComponentModel.DataAnnotations;

namespace Deposita.CashOperations.Application.DTOs
{
    public class UpdateProductDto
    {
        public decimal? Amount { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? TransactionType { get; set; }
    }
}
