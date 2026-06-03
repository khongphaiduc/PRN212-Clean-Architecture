using AutoMapper;
using Retail.Application.DTOs;
using Retail.Application.Interfaces;
using Retail.Application.Services;
using Retail_Application.Interfaces;
using Retail_Domain.Entities;
using Retail_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.ServicesImpl
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task AddProductAsync(ProductDto product)
        {
            var productEntity = _mapper.Map<ProductEntity>(product);
            _unitOfWork.productRepository.AddAsync(productEntity);
            _unitOfWork.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {

            var listProduct = await _unitOfWork.productRepository.GetAllAsync();

            return _mapper.Map<List<ProductDto>>(listProduct);
        }
    }
}
