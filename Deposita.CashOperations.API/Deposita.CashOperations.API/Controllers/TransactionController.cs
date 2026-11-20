using Deposita.CashOperations.Application.DTOs;
using Deposita.CashOperations.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deposita.CashOperations.API.Controllers
{

    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        /// <summary>Retrieves all transactions.</summary>
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _transactionService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        /// <summary>Retrieves a transaction by identifier.</summary>
        /// <param name="id">The transaction identifier.</param>
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _transactionService.GetByIdAsync(id));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        /// <summary>Creates a new transaction.</summary>
        /// <param name="request">The transaction data to create.</param>
        public async Task<IActionResult> Create(CreateProductDto request)
        {
            await _transactionService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, new
            {
                Message = "Transaction successfully created."
            });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        /// <summary>Updates an existing transaction.</summary>
        /// <param name="id">The transaction identifier.</param>
        /// <param name="request">The transaction data to update.</param>
        public async Task<IActionResult> Update(int id, UpdateProductDto request)
        {
            await _transactionService.UpdateAsync(id, request);
            return Ok(new
            {
                Message = "Transaction successfully updated."
            });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        /// <summary>Deletes an existing transaction.</summary>
        /// <param name="id">The transaction identifier.</param>
        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteAsync(id);
            return Ok(new
            {
                Message = "Transaction successfully deleted."
            });
        }
    }
}
