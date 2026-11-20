
using AutoMapper;
using Deposita.CashOperations.Application.DTOs;
using Deposita.CashOperations.Application.Exceptions;
using Deposita.CashOperations.Application.Interfaces.Repositories;
using Deposita.CashOperations.Application.Interfaces.Services;
using Deposita.CashOperations.Domain.Entities;
using System.Net;


namespace Deposita.CashOperations.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a transaction service.</summary>
        /// <param name="transactionRepository">The transaction repository.</param>
        /// <param name="mapper">The object mapper.</param>
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        /// <summary>Creates a transaction.</summary>
        /// <param name="dto">The transaction data to create.</param>
        public async Task CreateAsync(CreateProductDto dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
        }

        /// <summary>Deletes a transaction by identifier.</summary>
        /// <param name="id">The transaction identifier.</param>
        public async Task DeleteAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new AppException("Transaction not found", HttpStatusCode.NotFound);
              
            _transactionRepository.Delete(transaction);
            await _transactionRepository.SaveChangesAsync();
        }

        /// <summary>Retrieves all transactions.</summary>
        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(t => _mapper.Map<TransactionDto>(t));
        }

        /// <summary>Retrieves a transaction by identifier.</summary>
        /// <param name="id">The transaction identifier.</param>
        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new AppException("Transaction not found", HttpStatusCode.NotFound);

            return _mapper.Map<TransactionDto>(transaction);
        }

        /// <summary>Updates a transaction by identifier.</summary>
        /// <param name="id">The transaction identifier.</param>
        /// <param name="dto">The transaction data to update.</param>
        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if(transaction == null)
                throw new AppException("Transaction not found", HttpStatusCode.NotFound);

            var updateTransaction = _mapper.Map(dto, transaction);

            _transactionRepository.Update(updateTransaction);
            await _transactionRepository.SaveChangesAsync();
        }
    }
}
