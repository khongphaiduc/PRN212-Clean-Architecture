using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Retail_Application.Interfaces;
using Retail_Domain.Entities;
using Retail_Infastructure.Context;
using Retail_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Infastructure.Repo
{
    public class ProductRepository : Repository<ProductEntity, Product>, IProductRepository
    {
        public ProductRepository(ManagementRetailContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
        public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryId(int id)
        {
            var productList = await _dbContext.Products.Where(s => s.CategoryId == id).AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<ProductEntity>>(productList);
        }
    }
}
