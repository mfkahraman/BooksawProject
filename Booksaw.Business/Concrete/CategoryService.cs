using AutoMapper;
using Booksaw.Business.Abstract;
using Booksaw.DataAccess.Abstract;
using Booksaw.Dto.CategoryDtos;
using Booksaw.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booksaw.Business.Concrete
{
    public class CategoryService(ICategoryDal categoryDal, IMapper mapper) : ICategoryService
    {
        public void AddCategory(CreateCategoryDto dto)
        {
            var category = mapper.Map<Category>(dto);
            categoryDal.Add(category);
        }

        public void DeleteCategory(int id)
        {
            categoryDal.Delete(id);
        }

        public List<ResultCategoryDto> GetAllCategories()
        {
            var values = categoryDal.GetAll();
            return mapper.Map<List<ResultCategoryDto>>(values);
        }

        public ResultCategoryDto GetCategoryById(int id)
        {
            var category = categoryDal.GetById(id);
            return mapper.Map<ResultCategoryDto>(category);
        }

        public void UpdateCategory(UpdateCategoryDto dto)
        {
            var category = mapper.Map<Category>(dto);
            categoryDal.Update(category);
        }
    }
}
