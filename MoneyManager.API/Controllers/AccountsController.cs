using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("all")]
        public IEnumerable<Account> GetAll()
        {
            return _accountService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _accountService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _accountService.CreateAsync(account);
                return Created(new Uri(Url.Link("create", new { id = account.Id })), result);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _accountService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _accountService.PatchAsync(id, account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _accountService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _accountService.PatchAsync(id, account);
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
                await _accountService.DeleteAsync(id);
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
