using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
    }
}
