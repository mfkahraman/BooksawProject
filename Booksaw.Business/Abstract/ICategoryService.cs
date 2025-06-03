using Booksaw.Dto.CategoryDtos;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.Business.Abstract
{
    public interface ICategoryService
    {
        public void AddCategory(CreateCategoryDto dto);
        public void UpdateCategory(UpdateCategoryDto dto);
        public void DeleteCategory(int id);
        public ResultCategoryDto GetCategoryById(int id);
        public List<ResultCategoryDto> GetAllCategories();
    }
}
