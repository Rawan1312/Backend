// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("/api/v1/Payment")]
// public class PaymentController : ControllerBase
// {
//     private readonly PaymentService _PaymentServicee;

//     public PaymentController(PaymentService PaymentServicee)
//     {
//         _PaymentServicee = PaymentServicee;
//     }

//     // Get all orders
//     [HttpGet]
//     public async Task<IActionResult> GetAllPayment()
//     {
//         try
//         {
//             var payments = await _PaymentServicee.GetAllPaymentService();
//             var response = new { Message = "Return all Payment", Payments = payments };
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

//     // Get payment by ID
//     [HttpGet("{PaymentId}")]
//     public async Task<IActionResult> GetPaymentById(Guid PaymentId)
//     {
//         try
//         {
//             var payments= await _PaymentServicee.GetPaymentByIdService(PaymentId);
            
//             if (payments == null)
//             {
//                 return NotFound(new { Message = "Payment not found" });
//             }
            
//             return Ok(payments);
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

//     // Delete payment by ID
//     [HttpDelete("{Id}")]
//     public async Task<IActionResult> DeletePayment(Guid Id)
//     {
//         try
//         {
//             var result = await _PaymentServicee.DeletePaymentByIdService(Id);
//             if (result)
//             {
//                 return Ok(new { Message = "Payment deleted successfully" });
//             }
//             else
//             {
//                 return NotFound(new { Message = "Payment not found" });
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

//     // Create a new payment
//     [HttpPost]
//     public async Task<IActionResult> CreatePaymentDto([FromBody] CreatePaymentDto newpayment)
//     {
//         try
//         {
//             var CreatePaymentDto = await _PaymentServicee.CreatePaymentService(newpayment);
//             var response = new { Message = "payment created successfully", newpayment = CreatePaymentDto };
//             return Created($"/api/v1/payment/{CreatePaymentDto.PaymentId}", response);
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

//     // Update an existing payment
//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdatePaymentSerivce(Guid id, [FromBody] UpdatePaymentDto updatePayment)
//     {
//         try
//         {
//             if (updatePayment == null)
//             {
//                 return BadRequest("Invalid payment data.");
//             }

//             var updatPayments = await _PaymentServicee.UpdatePaymentService(id, updatePayment);
            
//             if (updatPayments == null)
//             {
//                 return NotFound(new { Message = "payment not found" });
//             }

//             var response = new { Message = "payment updated successfully", Payment = updatPayments };
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
