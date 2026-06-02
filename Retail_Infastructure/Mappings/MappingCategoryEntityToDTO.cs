using AutoMapper;
using Retail.Application.DTOs;
using Retail_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.Mappings
{
    public class MappingCategoryEntityToDTO : Profile
    {
        public MappingCategoryEntityToDTO()
        {
            CreateMap<CategoryEntity, ProductDto>();
        }
    }
}
