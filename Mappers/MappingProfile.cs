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

                      //Address Maps
            CreateMap<CreateAddressDto, Address>();
            CreateMap<Address, AddressDto?>(); //?? GeyByID
            CreateMap<UpdateAddress, Address>();
                   //Shipment Maps
             CreateMap<CreateShipment, Shipment>();
              CreateMap<Shipment, ShipmentDto?>();//??GeyByID
              CreateMap<UpdateShipment, Shipment>();

        }
    }
}