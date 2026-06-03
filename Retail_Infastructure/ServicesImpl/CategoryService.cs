using AutoMapper;
using Retail.Application.DTOs;
using Retail.Application.Services;
using Retail_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.ServicesImplement
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepo = categoryRepository;
            _productRepo = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var model = await _categoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(model);
        }

        public async Task<CategoryWithProductsDto?> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                return null;

            var products = await _productRepo.GetProductsByCategoryId(categoryId);

            var dto = new CategoryWithProductsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Products = _mapper.Map<List<ProductDto>>(products)        // map from ProductEntity to ProductDto
            };

            return dto;
        }



    }
}
