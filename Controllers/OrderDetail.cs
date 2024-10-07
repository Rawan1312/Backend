using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/orderdetails")]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderDetailService _orderdetailService;

    public OrderDetailController(IOrderDetailService orderdetailService)
    {
        _orderdetailService = orderdetailService;
    }

    //POST => /api/orderDetails => Create an orderDetail
    [HttpPost]
    public async Task<IActionResult> CreateOrderDetailDto([FromBody] CreateOrderDetailDto newOrderDetail)
    {
        if(!ModelState.IsValid)
     {   
          var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        //    return ApiResponse.BadRequest("Invalid orderdetail Data");
    }
        try
        {
            var createdOrderDetail = await _orderdetailService.CreateOrderDetailService(newOrderDetail);
            return ApiResponse.Created("orderdetail is created successfuly");
        }
        catch (ApplicationException ex)
        {
           return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);;
        }
    }
    // Get => /api/payments => RETURN all payment
    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetail()
    {
        try
        {
            var orderdetail = await _orderdetailService.GetAllOrderDetailService();
           if(orderdetail == null)
            {
            return ApiResponse.NotFound("orderDetail not found");
            }
         return ApiResponse.Success(orderdetail, "orderdetail are returned succesfully");
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

     //GET => /api/paymetns/{OrderDetailId} => return a single payment
    [HttpGet("{orderdetailId}")]
    public async Task<IActionResult> GetOrderDetailById(Guid orderdetailId)
    {
        try
        {
            var orderdetail = await _orderdetailService.GetOrderDetailByIdService(orderdetailId);
            
            if (orderdetail == null)
            {
                return ApiResponse.NotFound( "orderdetail not found" );;
            }
            
            return ApiResponse.Success(orderdetail,"orderdetail is retuned succcessfuly");
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

    // DELETE => /api/orderDetails/{orderdetailId} => delete a single orderdetail
    [HttpDelete("{orderdetailId}")]
    public async Task<IActionResult> DeleteOrderDetail(Guid orderdetailId)
    {
        try
        {
            var result = await _orderdetailService.DeleteOrderDetailByIdService(orderdetailId);
            if (result == false)
            {   return ApiResponse.NotFound($"orderdetail with this id {orderdetailId}dose not exist" );
            }
            else
            {
                 return ApiResponse.Success(result,"orderdetail is deleted successfuly");
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

    // PUT => /api/orderdetails/{orderdetailId} => Update an payment
    [HttpPut("{orderdetailId}")]
    public async Task<IActionResult> UpdateOrderDetailService(Guid orderdetailId, [FromBody] UpdateOrderDetailDto updateOrderDetail)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid orderdetail data.");
            }

            var updatedOrderdetail = await _orderdetailService.UpdateOrderdetailService(orderdetailId, updateOrderDetail);
            
            if (updatedOrderdetail == null)
            {
                return ApiResponse.NotFound("orderdetail not found.");
            }
             return ApiResponse.Success(updatedOrderdetail,"orderdetail is updated successfuly");
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