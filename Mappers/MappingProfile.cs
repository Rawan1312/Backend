using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
// using ecommerce_db_api.EFCore;
// using ecommerce_db_api.Models;
// using ecommerce_db_api.Models.categories;
// using ecommerce_db_api.Models.products;

namespace ecommerce_db_api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUser, User>();


            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();


        CreateMap<Product,ProductDto>();
         CreateMap<UpdateProductDto, Product>();
        CreateMap<CreateProductDto, Product>();


                      //Address Maps
            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap(); //?? GeyByID
            CreateMap<UpdateAddress, Address>();
                 
                   //Shipment Maps
             CreateMap<CreateShipment, Shipment>();
              CreateMap<Shipment, ShipmentDto>();//??GeyByID
              CreateMap<UpdateShipment, Shipment>();

              CreateMap<CreateOrderDto, Order>();
              CreateMap<Order, OrderDto>().ReverseMap();
              CreateMap<UpdateOrderDto, Order>();

        
    }
}}