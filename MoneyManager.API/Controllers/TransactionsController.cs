using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("all")]
        public IEnumerable<Transaction> GetAll()
        {
            return _transactionService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _transactionService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _transactionService.CreateAsync(transaction);
                return Created(new Uri(Url.Link("create", new { id = transaction.Id })), result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _transactionService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _transactionService.PatchAsync(id, transaction);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _transactionService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _transactionService.PatchAsync(id, transaction);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _transactionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
