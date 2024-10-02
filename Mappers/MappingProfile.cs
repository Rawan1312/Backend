using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace ecommerce_db_api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<UserDto,User>();
            CreateMap<CreateUserDto, User>();


            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();


        CreateMap<ProductDto,Product >();
         CreateMap<UpdateProductDto, Product>();
        CreateMap<CreateProductDto, Product>();

        }
    }
}