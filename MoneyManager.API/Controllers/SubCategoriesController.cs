using Microsoft.AspNetCore.Mvc;
using money_manager_api.Entities;
using money_manager_api.Services;

namespace money_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet("all")]
        public IEnumerable<SubCategory> GetAll()
        {
            return _subCategoryService.GetAll();
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = _subCategoryService.GetByIdAsync(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            try
            {
                var result = await _subCategoryService.CreateAsync(subCategory);
                return Created(new Uri(Url.Link("create", new { id = subCategory.Id })), result);
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
        public async Task<IActionResult> Put(Guid id, [FromBody] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _subCategoryService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _subCategoryService.PatchAsync(id, subCategory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request body.");

            var entity = await _subCategoryService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            try
            {
                var result = await _subCategoryService.PatchAsync(id, subCategory);
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
                await _subCategoryService.DeleteAsync(id);
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
