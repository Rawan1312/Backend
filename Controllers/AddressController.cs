using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/address")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
 public AddressController(IAddressService addressService)
 {
   _addressService = addressService;
 }
//   private readonly AddressService _addressService;
//     public AddressController(AppDBContext appDbContext) {
//     _addressService = new AddressService(appDbContext);

//? GET => /api/address => Get all the address
[HttpGet]

  public async Task<IActionResult> GetAllAddress(QueryParameters queryParameters)
    {
        try
        {
            var address = await _addressService.GetAllAddressService(queryParameters);
         //   var response=new{Message="return all the address",Address=address};
        return ApiResponse.Success(address,"Return Address is succesfully ");
        }
        catch (ApplicationException ex)
        {
            
            return ApiResponse.ServerError("Server Error"+ ex.Message);
        }
        catch (Exception ex){
            
            return ApiResponse.ServerError("Server Error"+ ex.Message);
        }}
    //? GET => /api/address/{id} => Get a single address by Id
    [HttpGet("{addressId}")]
    public async Task<IActionResult> GetSingleAddressById(Guid addressId)
    {
        //if (!Guid.TryParse(addressId, out Guid addressIdGuid))
       // {
        //    return BadRequest("Invalid address ID Format");
      //  }
        try {
        var address = await _addressService.GetAddresssByIdService(addressId);

        if (address == null)
        {
            return ApiResponse.NotFound($"address with {addressId} does not exist");
        }
        return ApiResponse.Success(address,"Return address by ID Successfly");
    }
    catch (ApplicationException ex)
    {
       return ApiResponse.ServerError("Server Error"+ ex.Message);
    }
    catch (Exception ex)
    {
         return ApiResponse.ServerError("Server Error"+ ex.Message);
    }
    }

  // //? Delete => /api/address/{id} => delete a single address by Id
    [HttpDelete("{addressId}")]
    public  async Task<IActionResult> DeleteaddressById(Guid addressId)
    {
       try{
        var result = await _addressService.DeleteAddressByIdService(addressId);
    
        if (result)
        {
            return ApiResponse.Success(result , "Delete Address ById Successfly");
        }
        else
        {
            return ApiResponse.NotFound("address not found" );
        }}
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("Server Error"+ ex.Message);
    }
    catch (Exception ex)
    {
         return ApiResponse.ServerError("Server Error"+ ex.Message);
    }
}


     [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody]CreateAddressDto newaddress) // Make sure that its AddressDto - NOT CreateAddressDto
    {
      try{
           var address = await _addressService.CreateAddressService(newaddress);
      // var response=new{Message="creat the address",Address=address};
        return ApiResponse.Created(address,"creat the address");
        // return ApiResponse.Success(address,"Create Address Successfly")
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("Server Error"+ ex.Message);
        }
        catch (Exception ex)
        {
           return ApiResponse.ServerError("Server Error"+ ex.Message);
        }
}
[HttpPut("{addressId}")]
public async Task<IActionResult> UpdateAddress(Guid addressId, [FromBody] UpdateAddress updateAddress)
{
    try
    {
        // تحقق من أن الكائن updateAddress ليس فارغًا
        if (updateAddress == null)
        {
            return ApiResponse.BadRequest("Invalid address data.");
        }

        // استخدام id كعنوان فريد
        var updatedAddress = await _addressService.UpdateAddressService(addressId, updateAddress);
        
        // تحقق مما إذا كان تم تحديث العنوان بنجاح
        if (updatedAddress == null)
        {
            return ApiResponse.NotFound("Address not found.");
        }

        return ApiResponse.Success(updatedAddress, "Address updated successfully");
    }
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("Server Error: " + ex.Message);
    }
    catch (Exception ex)
    {
        return ApiResponse.ServerError("Server Error: " + ex.Message);
    }
}

}