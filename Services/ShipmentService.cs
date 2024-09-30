using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



public class ShipmentService
  {
private readonly AppDBContext _appDbContext;
public ShipmentService(AppDBContext appDbContext){
  _appDbContext=appDbContext;
}      

    public async Task<List<Shipment>> GetAllShipmentService()
    {
      try
      {
        var shipment= await _appDbContext.Shipment.ToListAsync();
      return shipment;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the shipment table");
      }
    }
    public async Task<Shipment> CreateShipmentService(CreateShipment newshipment)
    {
      try
      {
        var shipment = new Shipment {
          ShipmentDate = newshipment.shipmentDate };
         await _appDbContext.Shipment.AddAsync(shipment);
         await _appDbContext.SaveChangesAsync();
         return shipment;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  shipment ");
      }
    }

    public async Task<ShipmentDto?> GetShipmentByIdService(Guid id)
{
    try
    {
        var shipment = await _appDbContext.Shipment
            .FirstOrDefaultAsync(u => u.ShipmentId == id);

        if (shipment == null)
        {
            return null; // Return null if user not found
        }

        // Convert User to UserDto if needed
        var shipmentDto = new ShipmentDto
        {
            ShipmentId = shipment.ShipmentId,
           
            // Map other properties as needed
        };

        return shipmentDto;
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the shipment.");
    }
}
   public async Task<bool> DeleteshipmentByIdService(Guid id)
{
    try
    {
        var shipmentToRemove = await _appDbContext.Shipment.FirstOrDefaultAsync(u => u.ShipmentId == id);

        if (shipmentToRemove != null)
        {
            _appDbContext.Shipment.Remove(shipmentToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the shipment.");
    }
}

    public async Task<Shipment> UpdateShipmentService(Guid id, UpdateShipment updateShipment)
{
    try
    {
        var existingShipment = await _appDbContext.Shipment.FindAsync(id);

        if (existingShipment == null)
        {
            throw new ApplicationException("Shipment not found.");
        }

        existingShipment.ShipmentDate = updateShipment.ShipmentDate ; 
        _appDbContext.Shipment.Update(existingShipment);
        await _appDbContext.SaveChangesAsync();

        return existingShipment;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the shipment.");
    }
}
    }