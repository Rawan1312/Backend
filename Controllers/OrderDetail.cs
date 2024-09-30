using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/orderDetail")]
public class OrderDetailController : ControllerBase
{
    private readonly OrderDetailService _orderdetailService;

    public OrderDetailController(OrderDetailService orderdetailService)
    {
        _orderdetailService = orderdetailService;
    }

    // Get all orders
    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetail()
    {
        try
        {
            var orderdetails = await _orderdetailService.GetAllOrderDetailService();
            var response = new { Message = "Return all ordersDetail", OrderDetails = orderdetails };
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

    // Get order by ID
    [HttpGet("{OrderDetailId}")]
    public async Task<IActionResult> GetOrderDetailById(Guid OrderDetailId)
    {
        try
        {
            var orderdetail = await _orderdetailService.GetOrderDetailByIdService(OrderDetailId);
            
            if (orderdetail == null)
            {
                return NotFound(new { Message = "OrderDetail not found" });
            }
            
            return Ok(orderdetail);
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

    // Delete order by ID
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteOrderDetail(Guid Id)
    {
        try
        {
            var result = await _orderdetailService.DeleteOrderDetailByIdService(Id);
            if (result)
            {
                return Ok(new { Message = "OrderDetail deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "OrderDetail not found" });
            }
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

    // Create a new order
    [HttpPost]
    public async Task<IActionResult> CreateOrderDetailDto([FromBody] CreateOrderDetailDto newOrderDetail)
    {
        try
        {
            var createdOrderDetail = await _orderdetailService.CreateOrderDetailService(newOrderDetail);
            var response = new { Message = "OrderDetail created successfully", OrderDetail = createdOrderDetail };
            return Created($"/api/v1/orderdetail/{createdOrderDetail.OrderDetailId}", response);
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

    // Update an existing order
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetailService(Guid id, [FromBody] UpdateOrderDetailDto updateOrderDetail)
    {
        try
        {
            if (updateOrderDetail == null)
            {
                return BadRequest("Invalid orderdetail data.");
            }

            var updatedOrderdetail = await _orderdetailService.UpdateOrderdetailService(id, updateOrderDetail);
            
            if (updatedOrderdetail == null)
            {
                return NotFound(new { Message = "OrderDetail not found" });
            }

            var response = new { Message = "OrderDetail updated successfully", OrderDetail = updatedOrderdetail };
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
