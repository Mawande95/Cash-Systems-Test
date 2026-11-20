

using Deposita.CashOperations.Application.DTOs;

namespace Deposita.CashOperations.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}
