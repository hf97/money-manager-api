using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly IParameterService _parameterService;

        public ParametersController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        [HttpGet("all")]
        public IEnumerable<Parameter> GetAll()
        {
            return _parameterService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _parameterService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("key/{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            var result = await _parameterService.GetByKeyAsync(key);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Parameter parameter)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _parameterService.CreateAsync(parameter);
                return Created(new Uri(Url.Link("create", new { key = parameter.Key })), result);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] Parameter parameter)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _parameterService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _parameterService.PatchAsync(id, parameter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Parameter parameter)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _parameterService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _parameterService.PatchAsync(id, parameter);
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
                await _parameterService.DeleteAsync(id);
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
