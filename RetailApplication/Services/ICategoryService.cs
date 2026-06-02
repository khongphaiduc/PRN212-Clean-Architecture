using Retail.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryWithProductsDto?> GetCategoryWithProductsAsync(int categoryId);

    }
}
