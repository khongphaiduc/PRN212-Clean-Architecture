using AutoMapper;
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
        }
    }
}
