using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
     //POST => /api/orders => Create an order
    [HttpPost]
public async Task<IActionResult> CreateOrderDto([FromBody] CreateOrderDto newOrder)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return BadRequest(new { Message = "Validation failed", Errors = errors });
    }

    try
    {
        var createdOrder = await _orderService.CreateOrderService(newOrder);
        return ApiResponse.Created(createdOrder,"Order is created successfully"); // Return created order
    }
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("Server error: " + ex.Message);
    }
    catch (Exception ex)
    {
        return ApiResponse.ServerError("Server error: " + ex.Message);
    }
}
    // Get => /api/orders => RETURN all order
    [HttpGet]
    public async Task<IActionResult> GetAllOrder()
    {
        try
        {
            var orders = await _orderService.GetAllOrderService();
            if(orders == null)
            {
                 {
            return ApiResponse.NotFound("orders not found");
            }
            }
            return ApiResponse.Success(orders, "order are returned succesfully");
        }
        catch (ApplicationException ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
    }
    //GET => /api/orders/{orderId} => return a single order
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid OrderId)
    {
        try
        {
            var order = await _orderService.GetOrderByIdService(OrderId);
            if (order == null)
            {
                  return ApiResponse.NotFound( "order not found" );
            }
            return ApiResponse.Success(order,"order is retuned succcessfuly");
        }
        catch (ApplicationException ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
           return ApiResponse.ServerError("server error:"+ ex.Message);
        }
    }
     // DELETE => /api/orders/{orderId} => delete a single order
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        try
        {
            var result = await _orderService.DeleteOrderByIdService(orderId);
            if (result== false)
            {
                return ApiResponse.NotFound($"orders with this id {orderId}dose not exist" );
                // return ApiResponse.NotFound($"order with this id {id}dose not exist" );
            }
            else
            {
                     return ApiResponse.Success(result,"order is deleted successfuly");
            }
        }
        catch (ApplicationException ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
    }
    // PUT => /api/payments/{orderId} => Update an order
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrderService(Guid orderId, [FromBody] UpdateOrderDto updateOrderDto)
    {    if (!ModelState.IsValid)
            {
              return ApiResponse.BadRequest("Invalid payment data.");
            }
        try
        {
            var updatedOrder = await _orderService.UpdateOrderService(orderId, updateOrderDto);
            if (updatedOrder == null)
            {
                return ApiResponse.NotFound("order not found.");
            }
            return ApiResponse.Success(updatedOrder,"order is updated successfuly");
        }
        catch (ApplicationException ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
    }
}