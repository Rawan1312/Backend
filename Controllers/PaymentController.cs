using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/Payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _PaymentServicee;

    public PaymentController(IPaymentService PaymentServicee)
    {
        _PaymentServicee = PaymentServicee;
    }

     //POST => /api/payments => Create an payment
     [HttpPost]
    public async Task<IActionResult> CreatePaymentDto([FromBody] CreatePaymentDto newpayment)
    {if(!ModelState.IsValid)
     {   
          var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        //    return ApiResponse.BadRequest("Invalid payment Data");
    }
        try
        {
            var CreatePayment = await _PaymentServicee.CreatePaymentService(newpayment);
           return ApiResponse.Created("payment is created successfuly");
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

    // Get => /api/payments => RETURN all payment
    [HttpGet]
    public async Task<IActionResult> GetAllPayment()
    {
        try
        {
            var payments = await _PaymentServicee.GetAllPaymentService();
            if(payments == null)
            {
            return ApiResponse.NotFound("payment not found");
            }
         return ApiResponse.Success(payments, "payment are returned succesfully");
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

    //GET => /api/paymetns/{paymentId} => return a single payment
    [HttpGet("{paymentId}")]
    public async Task<IActionResult> GetPaymentById(Guid paymentId)
    {
        try
        {
            var payments= await _PaymentServicee.GetPaymentByIdService(paymentId);
            
            if (payments == null)
            {
                 return ApiResponse.NotFound($"payment with this id {paymentId} does not exist" );
            }
            
            return ApiResponse.Success(payments,"payment is retuned succcessfuly");
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

    
 // DELETE => /api/payments/{paymentId} => delete a single payment
    [HttpDelete("{paymentId}")]
    public async Task<IActionResult> DeletePayment(Guid paymentId)
    {
        try
        {
            var result = await _PaymentServicee.DeletePaymentByIdService(paymentId);
            if (result == false)
            {
              return ApiResponse.NotFound($"payment with this id {paymentId}dose not exist" );
            }
            else
            {
               return ApiResponse.Success(result,"payment is deleted successfuly");
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


    // PUT => /api/payments/{paymentId} => Update an payment
    [HttpPut("{paymentId}")]
    public async Task<IActionResult> UpdatePaymentSerivce(Guid paymentId, [FromBody] UpdatePaymentDto updatePayment)
    {
            if (!ModelState.IsValid)
            {
              return ApiResponse.BadRequest("Invalid payment data.");
            }
           try
           {
            var updatPayments = await _PaymentServicee.UpdatePaymentService(paymentId, updatePayment);
            if (updatPayments == null)
            {
                return ApiResponse.NotFound("payment not found.");
            }
           return ApiResponse.Success(updatPayments,"payment is updated successfuly");}
        catch (ApplicationException ex)
        {
            return ApiResponse.ServerError("server error:"+ ex.Message);
        }
        catch (Exception ex)
        {
           return ApiResponse.ServerError("server error:"+ ex.Message);
        }
    
        
}}