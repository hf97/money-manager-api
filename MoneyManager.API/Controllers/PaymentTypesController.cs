using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypesController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet("all")]
        public IEnumerable<PaymentType> GetAll()
        {
            return _paymentTypeService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _paymentTypeService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _paymentTypeService.CreateAsync(paymentType);
                return Created(new Uri(Url.Link("create", new { id = paymentType.Id })), result);
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
        public async Task<IActionResult> Put(int id, [FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _paymentTypeService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _paymentTypeService.PatchAsync(id, paymentType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PaymentType parameter)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _paymentTypeService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _paymentTypeService.PatchAsync(id, parameter);
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
                await _paymentTypeService.DeleteAsync(id);
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
