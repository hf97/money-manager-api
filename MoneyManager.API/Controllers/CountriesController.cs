using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    public class CountriesController : Controller
    {
        [ApiController]
        [Route("[controller]")]
        public class ParametersController : ControllerBase
        {
            private readonly ICountryService _countryService;

            public ParametersController(ICountryService countryService)
            {
                _countryService = countryService;
            }

            [HttpGet("all")]
            public IEnumerable<Country> GetAll()
            {
                return _countryService.GetAll();
            }

            [HttpGet("id/{id}")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var result = _countryService.GetByIdAsync(id);

                return (result != null) ? Ok(result) : NotFound();
            }

            [HttpGet("iso/{iso}")]
            public async Task<IActionResult> GetByKey(string iso)
            {
                var result = await _countryService.GetByIsoAsync(iso);

                return (result != null) ? Ok(result) : NotFound();
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] Country country)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid request body.");

                try
                {
                    var result = await _countryService.CreateAsync(country);
                    return Created(new Uri(Url.Link("create", new { id = country.Id })), result);
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
            public async Task<IActionResult> Put(Guid id, [FromBody] Country country)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid request body.");

                var entity = await _countryService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound();

                try
                {
                    var result = await _countryService.PatchAsync(id, country);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpPatch("{id}")]
            public async Task<IActionResult> Patch(Guid id, [FromBody] Country country)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid request body.");

                var entity = await _countryService.GetByIdAsync(id);
                if (entity == null)
                    return NotFound();

                try
                {
                    var result = await _countryService.PatchAsync(id, country);
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
                    await _countryService.DeleteAsync(id);
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
}
