using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Application.DTOs
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}
