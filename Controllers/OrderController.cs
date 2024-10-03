// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("/api/v1/order")]
// public class OrderController : ControllerBase
// {
//     private readonly OrderService _orderService;

//     public OrderController(OrderService orderService)
//     {
//         _orderService = orderService;
//     }

//     // Get all orders
//     [HttpGet]
//     public async Task<IActionResult> GetAllOrder()
//     {
//         try
//         {
//             var orders = await _orderService.GetAllOrderService();
//             var response = new { Message = "Return all orders", Orders = orders };
//             return Ok(response);
//         }
//         catch (ApplicationException ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     // Get order by ID
//     [HttpGet("{OrderId}")]
//     public async Task<IActionResult> GetOrderById(Guid OrderId)
//     {
//         try
//         {
//             var order = await _orderService.GetOrderByIdService(OrderId);
            
//             if (order == null)
//             {
//                 return NotFound(new { Message = "Order not found" });
//             }
            
//             return Ok(order);
//         }
//         catch (ApplicationException ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     // Delete order by ID
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteOrder(Guid id)
//     {
//         try
//         {
//             var result = await _orderService.DeleteOrderByIdService(id);
//             if (result)
//             {
//                 return Ok(new { Message = "Order deleted successfully" });
//             }
//             else
//             {
//                 return NotFound(new { Message = "Order not found" });
//             }
//         }
//         catch (ApplicationException ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     // Create a new order
//     [HttpPost]
//     public async Task<IActionResult> CreateOrderDto([FromBody] CreateOrderDto newOrder)
//     {
//         try
//         {
//             var createdOrder = await _orderService.CreateOrderService(newOrder);
//             var response = new { Message = "Order created successfully", Order = createdOrder };
//             return Created($"/api/v1/order/{createdOrder.OrderId}", response);
//         }
//         catch (ApplicationException ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     // Update an existing order
//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdateOrderService(Guid id, [FromBody] UpdateOrderDto updateOrderDto)
//     {
//         try
//         {
//             if (updateOrderDto == null)
//             {
//                 return BadRequest("Invalid order data.");
//             }

//             var updatedOrder = await _orderService.UpdateOrderService(id, updateOrderDto);
            
//             if (updatedOrder == null)
//             {
//                 return NotFound(new { Message = "Order not found" });
//             }

//             var response = new { Message = "Order updated successfully", Order = updatedOrder };
//             return Ok(response);
//         }
//         catch (ApplicationException ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }
// }
