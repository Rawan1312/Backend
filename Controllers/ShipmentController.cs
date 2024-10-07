using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/shipment")]
public class ShipmentController : ControllerBase
//  private readonly ShipmentService _shipmentservice;
//     public ShipmentController(ShipmentService shipmentService)
//     {
//         _shipmentservice = shipmentservice;
//     }

{
     private readonly IShipmentService _shipmentService;
    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }
    // private readonly ShipmentService _shipmentService;
    // public ShipmentController(ShipmentService shipmentService)
    // {
    //     _shipmentService = shipmentService;
    // }
//? GET => /api/v1/shipment => Get all the shipments
    [HttpGet]
    public async Task<IActionResult> GetAllShipment()
    {
        try
        {
            var shipment = await _shipmentService.GetAllShipmentService();
         //   var response=new{Message="return all the shipment",Shipment=shipment};
        return ApiResponse.Success(shipment,"return all the shipment");
        }
        catch (ApplicationException ex)
        {
            
            return ApiResponse.ServerError("Server Error"+ ex.Message);
        }
        catch (Exception ex){
            
            return ApiResponse.ServerError("Server Error"+ ex.Message);
        }}

[HttpGet("{ShipmentId}")]
public async Task<IActionResult> GetShipmentById(Guid ShipmentId)
{
    try
    {
        var shipment = await _shipmentService.GetShipmentByIdService(ShipmentId);
        
        if (shipment == null)
        {
            return ApiResponse.NotFound("Shipment not found" );
        }
        
        return ApiResponse.Success(shipment);
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
[HttpDelete("{ShipmentId}")]
public async Task<IActionResult> DeleteShipment(Guid ShipmentId)
{
    try
    {
        var result = await _shipmentService.DeleteshipmentByIdService(ShipmentId);
        if (result)
        {
            return ApiResponse.Success(result, "shipment deleted successfully" );
        }
        else
        {
            return ApiResponse.NotFound("shipment not found");
        }}
    catch (ApplicationException ex)
    {
         return ApiResponse.ServerError("Server Error"+ ex.Message);
    }
    catch (Exception ex)
    {
        return ApiResponse.ServerError("Server Error"+ ex.Message);
    }}
[HttpPost]
    public async Task<IActionResult> CreatShipment([FromBody]CreateShipment newshipment)
    {
        try
        {
            var shipment = await _shipmentService.CreateShipmentService(newshipment);
            //var response=new{Message="creat the shipment",Shipment=shipment};
            return ApiResponse.Created(shipment,"creat the shipment");
       // return ApiResponse.Created(response,$"/api/shipment/{shipment.ShipmentId}")
        
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("Server Error"+ ex.Message);
        }
        catch (Exception ex)
        {
             return ApiResponse.ServerError("Server Error"+ ex.Message);
        }}
[HttpPut("{ShipmentId}")]
public async Task<IActionResult> UpdateShipment(Guid id, [FromBody] UpdateShipment updateShip)
{
    try
    {
        if (updateShip == null)
       {
          return ApiResponse.BadRequest("Invalid shipment data.");}

        var updateShipment = await _shipmentService.UpdateShipmentService(id, updateShip);
        
        if (updateShipment == null)
        {
            return ApiResponse.NotFound("shipment not found.");
        }

        // var response = new { Message = "shipment updated successfully", Shipment = updateShipment };
        return ApiResponse.Success(updateShipment , "shipment updated successfully");
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
}
