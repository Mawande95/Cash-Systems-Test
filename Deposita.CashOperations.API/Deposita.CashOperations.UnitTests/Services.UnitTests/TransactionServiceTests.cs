

using AutoMapper;
using Deposita.CashOperations.Application.DTOs;
using Deposita.CashOperations.Application.Exceptions;
using Deposita.CashOperations.Application.Interfaces.Repositories;
using Deposita.CashOperations.Application.Mapping;
using Deposita.CashOperations.Application.Services;
using Deposita.CashOperations.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;

namespace Deposita.CashOperations.UnitTests.Services.UnitTests
{
    public class TransactionServiceTests
    {
        private readonly FakeTransactionRepository _repository = new();
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
                configuration.AddProfile<TransactionProfile>(),
                NullLoggerFactory.Instance);

            _service = new TransactionService(_repository, mapperConfiguration.CreateMapper());
        }

        [Fact]
        public async Task CreateAsync_AddsMappedTransactionAndSavesChanges()
        {
            var request = new CreateProductDto
            {
                Amount = 125.50m,
                Description = "Cash deposit",
                TransactionType = "Deposit"
            };

            await _service.CreateAsync(request);

            var transaction = Assert.Single(_repository.AddedTransactions);
            Assert.Equal(request.Amount, transaction.Amount);
            Assert.Equal(request.Description, transaction.Description);
            Assert.Equal(request.TransactionType, transaction.TransactionType);
            Assert.Equal(1, _repository.SaveChangesCallCount);
        }

        [Fact]
        public async Task DeleteAsync_WhenTransactionExists_DeletesTransactionAndSavesChanges()
        {
            var transaction = CreateTransaction(1);
            _repository.Transactions.Add(transaction);

            await _service.DeleteAsync(transaction.TransactionId);

            Assert.Same(transaction, Assert.Single(_repository.DeletedTransactions));
            Assert.Equal(1, _repository.SaveChangesCallCount);
        }

        [Fact]
        public async Task DeleteAsync_WhenTransactionDoesNotExist_ThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<AppException>(() => _service.DeleteAsync(99));

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Empty(_repository.DeletedTransactions);
            Assert.Equal(0, _repository.SaveChangesCallCount);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedTransactions()
        {
            _repository.Transactions.AddRange([
                CreateTransaction(1, 10m, "Deposit", "Cash deposit"),
            CreateTransaction(2, 20m, "Withdrawal", "Cash withdrawal")]);

            var result = (await _service.GetAllAsync()).ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(1, result[0].TransactionId);
            Assert.Equal(10m, result[0].Amount);
            Assert.Equal("Cash withdrawal", result[1].Description);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTransactionExists_ReturnsMappedTransaction()
        {
            var transaction = CreateTransaction(1, 50m, "Deposit", "Cash deposit");
            _repository.Transactions.Add(transaction);

            var result = await _service.GetByIdAsync(transaction.TransactionId);

            Assert.NotNull(result);
            Assert.Equal(transaction.TransactionId, result.TransactionId);
            Assert.Equal(transaction.DateCreated, result.DateCreated);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.Description, result.Description);
            Assert.Equal(transaction.TransactionType, result.TransactionType);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTransactionDoesNotExist_ThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<AppException>(() => _service.GetByIdAsync(99));

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenTransactionExists_UpdatesMappedValuesAndSavesChanges()
        {
            var transaction = CreateTransaction(1, 50m, "Deposit", "Original description");
            _repository.Transactions.Add(transaction);
            var request = new UpdateProductDto
            {
                Amount = 75m,
                Description = "Updated description"
            };

            await _service.UpdateAsync(transaction.TransactionId, request);

            Assert.Same(transaction, Assert.Single(_repository.UpdatedTransactions));
            Assert.Equal(75m, transaction.Amount);
            Assert.Equal("Updated description", transaction.Description);
            Assert.Equal("Deposit", transaction.TransactionType);
            Assert.Equal(1, _repository.SaveChangesCallCount);
        }

        [Fact]
        public async Task UpdateAsync_WhenTransactionDoesNotExist_ThrowsNotFoundException()
        {
            var exception = await Assert.ThrowsAsync<AppException>(() =>
                _service.UpdateAsync(99, new UpdateProductDto { Description = "Updated" }));

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
            Assert.Empty(_repository.UpdatedTransactions);
            Assert.Equal(0, _repository.SaveChangesCallCount);
        }

        private static Transaction CreateTransaction(
            int id,
            decimal amount = 100m,
            string transactionType = "Deposit",
            string description = "Test transaction") => new()
            {
                TransactionId = id,
                DateCreated = new DateTime(2026, 1, 1),
                Amount = amount,
                Description = description,
                TransactionType = transactionType
            };

        private sealed class FakeTransactionRepository : ITransactionRepository
        {
            public List<Transaction> Transactions { get; } = [];
            public List<Transaction> AddedTransactions { get; } = [];
            public List<Transaction> UpdatedTransactions { get; } = [];
            public List<Transaction> DeletedTransactions { get; } = [];
            public int SaveChangesCallCount { get; private set; }

            public Task<IEnumerable<Transaction>> GetAllAsync() =>
                Task.FromResult<IEnumerable<Transaction>>(Transactions);

            public Task<Transaction?> GetByIdAsync(int id) =>
                Task.FromResult(Transactions.SingleOrDefault(transaction => transaction.TransactionId == id));

            public Task AddAsync(Transaction entity)
            {
                AddedTransactions.Add(entity);
                return Task.CompletedTask;
            }

            public void Update(Transaction entity) => UpdatedTransactions.Add(entity);

            public void Delete(Transaction entity) => DeletedTransactions.Add(entity);

            public Task SaveChangesAsync()
            {
                SaveChangesCallCount++;
                return Task.CompletedTask;
            }
        }
    }
}
