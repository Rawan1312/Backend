using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AddressService
  {
  private readonly AppDBContext _appDbContext;
public AddressService(AppDBContext appDbContext){
  _appDbContext=appDbContext;
}
    public async Task<List<Address>> GetAllAddressService() {
      try{
        var address= await _appDbContext.Address.ToListAsync();
      return address;
      }
       catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the address table");
      }
    } 
      
    //public List<AddressDto> GetAllAddressService()
    //{
    ///  return _Address;
    //}

    public async Task<AddressDto?> GetAddresssByIdService(Guid id)
    {
      try{
      var address = await _appDbContext.Address
            .FirstOrDefaultAsync(d => d.AddressId == id);

        if (address == null)
        {
            return null; 
        }
        
        var addressDto = new AddressDto
        {
            AddressId = address.AddressId,
            City = address.City,
            State = address.State,
            // Map other properties as needed
        };

        return addressDto;
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the address.");
    }}
    public async Task<AddressDto> CreateAddressService(CreateAddressDto newAddress)
    {
      try{
        var address = new AddressDto
        {
       AddressId = Guid.NewGuid(),
        City = newAddress.City, 
        State = newAddress.State // user must add city & state => its re
        };
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
    public async Task<AddressDto> UpdateAddressService(Guid id, AddressDto updateAddress)
{
    try
    {
        var existingaddress = await _appDbContext.Address.FindAsync(id);

        if (existingaddress == null)
        {
            throw new ApplicationException("address not found.");
        }

        existingaddress.City = updateAddress.City ?? existingaddress.City;
        existingaddress.State = updateAddress.State ?? existingaddress.State;

        _appDbContext.Address.Update(existingaddress);
        await _appDbContext.SaveChangesAsync();

        return existingaddress;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the address.");
    }
}
   }