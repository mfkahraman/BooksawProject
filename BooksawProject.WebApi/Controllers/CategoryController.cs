using Booksaw.Business.Abstract;
using Booksaw.Business.Concrete;
using Booksaw.Dto.BookDtos;
using Booksaw.Dto.CategoryDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksawProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var values = categoryService.GetAllCategories();
            if (values == null || !values.Any())
            {
                return NotFound("No category found.");
            }
            return Ok(values);
        }

        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var value = categoryService.GetCategoryById(id);
            if (value == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(value);
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CreateCategoryDto dto)
        {

            categoryService.AddCategory(dto);
            return Ok("Kategori başarıyla oluşturuldu");
        }

        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory(UpdateCategoryDto dto)
        {
            categoryService.UpdateCategory(dto);
            return Ok("Kategori başarıyla güncellendi");
        }

        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            categoryService.DeleteCategory(id);
            return Ok("Kategori başarıyla silindi");
        }

    }
}
