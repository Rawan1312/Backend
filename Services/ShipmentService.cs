using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IShipmentService{
    Task<List<Shipment>> GetAllShipmentService();
    Task<ShipmentDto?> GetShipmentByIdService(Guid Id);
    Task<bool> DeleteshipmentByIdService(Guid id);
    Task<Shipment> CreateShipmentService(CreateShipment newshipment);
     Task<Shipment> UpdateShipmentService(Guid id, UpdateShipment updateShipment);
}
public class ShipmentService:IShipmentService
  {
private readonly AppDBContext _appDbContext;
 private readonly IMapper _mapper;
public ShipmentService(AppDBContext appDbContext , IMapper mapper){
  _appDbContext=appDbContext;
   _mapper = mapper ;
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
        var shipment = _mapper.Map<Shipment>(newshipment);
        //   ShipmentDate = newshipment.shipmentDate };
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
        var shipment = _mapper.Map<ShipmentDto?>(id);
        await _appDbContext.Shipment .FirstOrDefaultAsync(u => u.ShipmentId == id);

        if (shipment == null)
        {
            return null; // Return null if shipment not found
        }

        // Convert shipment to ShipmentDto if needed
       // var shipmentDto = new ShipmentDto
        // {
        //     ShipmentId = shipment.ShipmentId,
        //     CompanyName = shipment.CompanyName,
           
        //     // Map other properties as needed
        // };

        return _mapper.Map<ShipmentDto?>(shipment);
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
        _mapper.Map(updateShipment ,existingShipment );
        //existingShipment.ShipmentDate = updateShipment.ShipmentDate ; 
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