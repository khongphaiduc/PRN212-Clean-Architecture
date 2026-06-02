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
    public class MappingProductEntityToDTO : Profile
    {
        public MappingProductEntityToDTO()
        {
            CreateMap<ProductEntity, ProductDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));
        }
    }
}
