// Controllers/CategoriesController.cs

using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            if(categories is null)
                return NotFound("");
            else
                return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategorybyId(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            if(categories is null)
                return NotFound("Category not found");
            else
                return Ok(categories);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto is null)
                return BadRequest("Invalid Data");
           
            await _categoryService.Add(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
            
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest();
            }
            if (categoryDto is null)
            {
                return BadRequest();
            }

            await _categoryService.Update(categoryDto);
            return Ok(categoryDto);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category is null)
                return NotFound("Category not found");

            await _categoryService.Remove(id);
            return Ok(category);
        }
    }
}
