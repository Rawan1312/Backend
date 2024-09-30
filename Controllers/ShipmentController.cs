using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/shipment")]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentService _shipmentService;
    public ShipmentController(ShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShipment()
    {
        try
        {
            var shipment = await _shipmentService.GetAllShipmentService();
            var response=new{Message="return all the shipment",Shipment=shipment};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex){
            
            return StatusCode(500, ex.Message);
        }}

[HttpGet("{ShipmentId}")]
public async Task<IActionResult> GetShipmentById(Guid ShipmentId)
{
    try
    {
        var shipment = await _shipmentService.GetShipmentByIdService(ShipmentId);
        
        if (shipment == null)
        {
            return NotFound(new { Message = "Shipment not found" });
        }
        
        return Ok(shipment);
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
[HttpDelete("{ShipmentId}")]
public async Task<IActionResult> DeleteShipment(Guid ShipmentId)
{
    try
    {
        var result = await _shipmentService.DeleteshipmentByIdService(ShipmentId);
        if (result)
        {
            return Ok(new { Message = "shipment deleted successfully" });
        }
        else
        {
            return NotFound(new { Message = "shipment not found" });
        }}
    catch (ApplicationException ex)
    {
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }}
[HttpPost]
    public async Task<IActionResult> CreatShipment([FromBody]CreateShipment newshipment)
    {
        try
        {
            var shipment = await _shipmentService.CreateShipmentService(newshipment);
            var response=new{Message="creat the shipment",Shipment=shipment};
        return Created($"/api/shipment/{shipment.ShipmentId}",response);
        }
        catch (ApplicationException ex)
        {
            
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }}
[HttpPut("{ShipmentId}")]
public async Task<IActionResult> UpdateShipment(Guid id, [FromBody] UpdateShipment updateShipment)
{
    try
    {
        if (updateShipment == null)
       {
          return BadRequest("Invalid shipment data.");}

        var updateShipment = await _shipmentService.UpdateShipmentService(id, updateShipment);
        
        if (updateShipment == null)
        {
            return NotFound("shipment not found.");
        }

        var response = new { Message = "shipment updated successfully", Shipment = updateShipment };
        return Ok(response);
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
}
