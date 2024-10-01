using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/Address")]
public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;
 public AddressController(AddressService addressService)
 {
   _addressService = addressService;
 }
//   private readonly AddressService _addressService;
//     public AddressController(AppDBContext appDbContext) {
//     _addressService = new AddressService(appDbContext);

//? GET => /api/address => Get all the address
[HttpGet]

  public async Task<IActionResult> GetAllAddress()
    {
        try
        {
            var address = await _addressService.GetAllAddressService();
            var response=new{Message="return all the address",Address=address};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex){
            
            return StatusCode(500, ex.Message);
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
            return NotFound($"address with {addressId} does not exist");
        }
        return Ok(address);
    }
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
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
            return Ok(new { Message = "address deleted successfully" });
        }
        else
        {
            return NotFound(new { Message = "address not found" });
        }}
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}


     [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody]CreateAddressDto newaddress) // Make sure that its AddressDto - NOT CreateAddressDto
    {
      try{
           var address = await _addressService.CreateAddressService(newaddress);
       var response=new{Message="creat the address",Address=address};
        return Created($"/api/address/{address.AddressId}",response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
}
[HttpPut("{addressId}")]
public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddress updateAddress)
{
    try
    {
        if (updateAddress == null)
        {
            return BadRequest("Invalid address data.");}

        var updatedAddress = await _addressService.UpdateAddressService(id, updateAddress);
        
        if (updateAddress == null)
        {
            return NotFound("address not found.");
        }

        var response = new { Message = "Address updated successfully", Address = updatedAddress };
        return Ok(response);
    }
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }}

}