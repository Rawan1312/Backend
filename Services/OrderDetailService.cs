using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



public class OrderDetailService
  {
private readonly AppDBContext _appDbContext;
public OrderDetailService(AppDBContext appDbContext){
  _appDbContext=appDbContext;
}      

    public async Task<List<OrderDetail>> GetAllOrderDetailService()
    {
      try
      {
        var orderdetail= await _appDbContext.OrderDetails.ToListAsync();
      return orderdetail;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the orderdetail table");
      }
    }
      // Create a new order
    public async Task<OrderDetail> CreateOrderDetailService(CreateOrderDetailDto newOrderdetail)
    {
        try
        {
            var orderdetail = new OrderDetail
            {
               TotalPrice=newOrderdetail.TotalPrice,
               
               Quantity= newOrderdetail.Quantity         
            };

            await _appDbContext.OrderDetails.AddAsync(orderdetail);
            await _appDbContext.SaveChangesAsync();

            return orderdetail;
        }
        catch (Exception)
        {
            throw new ApplicationException("Error occurred when creating the orderdetail.");
        }
    }

    public async Task<OrderDetailDto?> GetOrderDetailByIdService(Guid OrderDetailId)
{
    try
    {
        var orderdetail = await _appDbContext.OrderDetails
            .FirstOrDefaultAsync(d => d.OrderDetailId == OrderDetailId);

        if (orderdetail == null)
        {
            return null; // Return null if user not found
        }

        // Convert User to orderdetailDto if needed
        var orderdetailDto = new OrderDetailDto
        {
          OrderDetailId = orderdetail.OrderDetailId,
          TotalPrice = orderdetail.TotalPrice,
          Quantity = orderdetail.Quantity,
            // Map other properties as needed
        };

        return orderdetailDto;
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the orderDetail.");
    }
}
   public async Task<bool> DeleteOrderDetailByIdService(Guid id)
{
    try
    {
        var OrderDetailToRemove = await _appDbContext.OrderDetails.FirstOrDefaultAsync(d => d.OrderDetailId == id);

        if (OrderDetailToRemove != null)
        {
            _appDbContext.OrderDetails.Remove(OrderDetailToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the orderdetail.");
    }
}

    public async Task<OrderDetail> UpdateOrderdetailService(Guid id, UpdateOrderDetailDto updateOrderDetailDto)
{
    try
    {
        var existingorder = await _appDbContext.OrderDetails.FindAsync(id);

        if (existingorder == null)
        {
            throw new ApplicationException("orderdetail not found.");
        }

        existingorder.TotalPrice = updateOrderDetailDto.TotalPrice;
        existingorder.Quantity = updateOrderDetailDto.Quantity ;
       

        _appDbContext.OrderDetails.Update(existingorder);
        await _appDbContext.SaveChangesAsync();

        return existingorder;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the orderdetail.");
    }
}
    }