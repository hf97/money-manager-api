using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("all")]
        public IEnumerable<Currency> GetAll()
        {
            return _currencyService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _currencyService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("iso/{iso}")]
        public async Task<IActionResult> GetByKey(string iso)
        {
            var result = await _currencyService.GetByIsoAsync(iso);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _currencyService.CreateAsync(currency);
                return Created(new Uri(Url.Link("create", new { id = currency.Id })), result);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _currencyService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _currencyService.PatchAsync(id, currency);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _currencyService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _currencyService.PatchAsync(id, currency);
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
                await _currencyService.DeleteAsync(id);
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
