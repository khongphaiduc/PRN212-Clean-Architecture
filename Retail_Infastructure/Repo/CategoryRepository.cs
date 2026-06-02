using Retail_Application.Interfaces;
using Retail_Domain.Entities;
using Retail_Infastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Infastructure.Repo
{
    public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ManagementRetailContext dbContext) : base(dbContext)
        {
        }
    }
}
