using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IAddressService{
    Task<List<Address>> GetAllAddressService();
    Task<AddressDto?> GetAddressByIdService(Guid AddressId);
    Task<bool> DeleteAddressByIdService(Guid id);
    Task<Address> CreateAddressService(CreateAddressDto newaddress);
    Task<Address> UpdateAddressService(Guid id, AddressDto updateAddressDto);
    }

public class AddressService
  {
  private readonly AppDBContext _appDbContext;

  private readonly IMapper _mapper;
public AddressService(AppDBContext appDbContext , IMapper mapper ){
  _appDbContext=appDbContext; 
  _mapper = mapper ; 
}
    public async Task<List<AddressDto>> GetAllAddressService() {
      try{
        var address= await _appDbContext.Address.Include(u => u.User).ToListAsync();
      var addressDtos = _mapper.Map<List<AddressDto>>(address);
        return addressDtos;
      }
       catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the address table");
      }}
    public async Task<AddressDto?> GetAddresssByIdService(Guid id)
    {
      try{
         var address = _mapper.Map<AddressDto?>(id);
       await _appDbContext.Address 
            .FirstOrDefaultAsync(d => d.AddressId == id);

        if (address == null)
        {
            return null; 
        }
        
        // var addressDto = new AddressDto   // Make sure
        // {
        //     AddressId = address.AddressId,
        //     City = address.City,
        //     State = address.State,
        //     // Map other properties as needed
        // };_mapper.Map<Address>(newAddress);

        return _mapper.Map<AddressDto?>(address); // make sure
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the address.");
    }}
    public async Task<Address> CreateAddressService(CreateAddressDto newAddress)
    {
      // Make sure AddressDto newAddress & Task<Address> I think CreateAddressDto newAddress
      try{
        var address = _mapper.Map<Address>(newAddress);

       // user must add city & state 
      
      //_Address.Add(address);
      await _appDbContext.Address.AddAsync(address);
         await _appDbContext.SaveChangesAsync();
      return address;
    }
     catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  address ");
      } }
    public async Task<bool> DeleteAddressByIdService(Guid id)
    {
      try{
      var addressToRemove = await _appDbContext.Address.FirstOrDefaultAsync(adr => adr.AddressId == id);
      
      if (addressToRemove != null)
      {
        _appDbContext.Address.Remove(addressToRemove);
            await _appDbContext.SaveChangesAsync();
            return true;
      }
      return false;
    }
     catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the address.");
    }
   } 
    public async Task<Address> UpdateAddressService(Guid id, UpdateAddress updateAddress)
    // Make sure change From (Task<Address>) AddressDto => Address
    // before editung was variable updateAddress from AddressDto
{
    try
    {
      //  var address = _mapper.Map<Address>(updateAddress ,existingaddress );
        var existingaddress = await _appDbContext.Address.FindAsync(id);

        if (existingaddress == null)
        {
            throw new ApplicationException("address not found.");
        }

        // existingaddress.City = updateAddress.City ?? existingaddress.City;
        // existingaddress.State = updateAddress.State ?? existingaddress.State;
       _mapper.Map(updateAddress ,existingaddress );
        _appDbContext.Address.Update(existingaddress);
        await _appDbContext.SaveChangesAsync();

        return existingaddress;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the address.");
    }
}}