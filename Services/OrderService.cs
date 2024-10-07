using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IOrderService{
    Task<List<Order>> GetAllOrderService();
    Task<Order> CreateOrderService(CreateOrderDto newOrder);
    Task<OrderDto?> GetOrderByIdService(Guid OrderId);
    Task<bool> DeleteOrderByIdService(Guid id);
    Task<Order> UpdateOrderService(Guid id, UpdateOrderDto updateOrder);
}
public class OrderService:IOrderService
  {
private readonly AppDBContext _appDbContext;
private readonly IMapper _mapper;
public OrderService(AppDBContext appDbContext,IMapper mapper){
  _appDbContext=appDbContext;
  _mapper= mapper;
}      

    public async Task<List<Order>> GetAllOrderService()
    {
      try
      {
        var order= await _appDbContext.Orders.Include(u => u.User).ToListAsync();
      return order;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the order table");
      }
    }
      // Create a new order
    public async Task<Order> CreateOrderService(CreateOrderDto newOrder)
    {
        try
        {
            var order = _mapper.Map<Order>(newOrder);

            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
            return order;   }
        catch (Exception)
        {
            throw new ApplicationException("Error occurred when creating the order.");
        }
    }

    public async Task<OrderDto?> GetOrderByIdService(Guid OrderId)
{
    try
    {
        var order = await _appDbContext.Orders
            .FirstOrDefaultAsync(d => d.OrderId == OrderId);

        if (order == null)
        {
            return null; // Return null if order not found
        }
        return _mapper.Map<OrderDto>(order);
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the order.");
    }
}
   public async Task<bool> DeleteOrderByIdService(Guid id)
{
    try
    {
        var OrderToRemove = await _appDbContext.Orders.FirstOrDefaultAsync(d => d.OrderId == id);

        if (OrderToRemove != null)
        {
            _appDbContext.Orders.Remove(OrderToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the order.");
    }
}

    public async Task<Order> UpdateOrderService(Guid id, UpdateOrderDto updateOrder)
{
    try
    {
        var existingorder = await _appDbContext.Orders.FindAsync(id);

        if (existingorder == null)
        {
            throw new ApplicationException("order not found.");
        }
        
         _mapper.Map(updateOrder, existingorder);
        _appDbContext.Orders.Update(existingorder);
        await _appDbContext.SaveChangesAsync();

        return existingorder;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the order.");
    }
}
    }