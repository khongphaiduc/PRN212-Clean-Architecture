using AutoMapper;
using Retail_Domain.Entities;
using Retail_Infastructure.Models;


namespace Retail.Infastructure.Mappings
{
    // 3 thứ cần Map trong Clean Architecture:
    //  Domain Entity <-> Database Model (EF)
    //  Domain Entity <-> DTO (Data Transfer Object)
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryEntity>()
                .ConstructUsing(src => new CategoryEntity(
                    src.Id,
                    src.Name,
                    src.Description
                ));

            CreateMap<CategoryEntity, Category>();   //  map reverse
        }
    }
}
