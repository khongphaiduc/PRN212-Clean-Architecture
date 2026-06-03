using AutoMapper;
using Retail.Application.DTOs;
using Retail_Domain.Entities;
using Retail_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.Mappings
{
    public class ProdutProfile : Profile
    {
        public ProdutProfile()
        {
            CreateMap<Product, ProductEntity>().ConstructUsing(s => new ProductEntity(

                s.Id, s.Name, s.Price, s.Quantity, s.CategoryId));

            CreateMap<ProductEntity, Product>();


            CreateMap<ProductEntity, ProductDto>().ForMember(s => s.Id, a => a.MapFrom(t => t.Id))
               .ForMember(s => s.Id, a => a.MapFrom(t => t.Id)).
                ForMember(s => s.CategoryId, a => a.MapFrom(t => t.CategoryId)).
                ForMember(s => s.Price, a => a.MapFrom(t => t.Price)).
                ForMember(s => s.Quantity, a => a.MapFrom(t => t.Quantity)).
                ForMember(s => s.Name, a => a.MapFrom(t => t.Name));
            CreateMap<ProductDto, ProductEntity>();


        }
    }
}
