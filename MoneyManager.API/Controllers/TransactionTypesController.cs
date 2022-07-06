using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionTypesController : ControllerBase
    {
        private readonly ITransactionTypeService _TransactionTypeService;

        public TransactionTypesController(ITransactionTypeService TransactionTypeService)
        {
            _TransactionTypeService = TransactionTypeService;
        }

        [HttpGet("all")]
        public IEnumerable<TransactionType> GetAll()
        {
            return _TransactionTypeService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _TransactionTypeService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionType transactionType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _TransactionTypeService.CreateAsync(transactionType);
                return Created(new Uri(Url.Link("create", new { id = transactionType.Id })), result);
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
        public async Task<IActionResult> Put(int id, [FromBody] TransactionType transactionType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _TransactionTypeService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _TransactionTypeService.PatchAsync(id, transactionType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] TransactionType transactionType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _TransactionTypeService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _TransactionTypeService.PatchAsync(id, transactionType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _TransactionTypeService.DeleteAsync(id);
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
