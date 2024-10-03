// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Extensions;



// public class PaymentService
//   {
// private readonly AppDBContext _appDbContext;
// public PaymentService(AppDBContext appDbContext){
//   _appDbContext=appDbContext;
// }      

//     public async Task<List<Payment>> GetAllPaymentService()
//     {
//       try
//       {
//         var payment= await _appDbContext.Payments.ToListAsync();
//       return payment;
//       }
//       catch (System.Exception)
//       {
        
//         throw new ApplicationException("erorr ocurred when get the data from the payment table");
//       }
//     }
//       // Create a new payment
//     public async Task<Payment> CreatePaymentService(CreatePaymentDto newpayment)
//     {
//         try
//         {
//             var payment = new Payment
//             {
//                PaymentId=newpayment.PaymentId,
               
//                Amount= newpayment.Amount,
//                 PaymentMethods = newpayment.PaymentMethods   
//             };

//             await _appDbContext.Payments.AddAsync(payment);
//             await _appDbContext.SaveChangesAsync();

//             return payment;
//         }
//         catch (Exception)
//         {
//             throw new ApplicationException("Error occurred when creating the Payment.");
//         }
//     }

//     public async Task<PaymentDto?> GetPaymentByIdService(Guid PaymentId)
// {
//     try
//     {
//         var payment = await _appDbContext.Payments
//             .FirstOrDefaultAsync(p => p.PaymentId == PaymentId);

//         if (payment == null)
//         {
//             return null; // Return null if payment  not found
//         }

//         // Convert payment to paymentDto if needed
//         var paymentDto = new PaymentDto
//         {
//           PaymentId = payment.PaymentId,
//           Amount = payment.Amount,
//           paymentMethods =payment.PaymentMethods
//             // Map other properties as needed
//         };

//         return paymentDto;
//     }
//     catch (Exception)
//     {
//         throw new ApplicationException("Error occurred while retrieving the payment.");
//     }
// }
//    public async Task<bool> DeletePaymentByIdService(Guid id)
// {
//     try
//     {
//         var PaymentToRemove = await _appDbContext.Payments.FirstOrDefaultAsync(d => d.PaymentId == id);

//         if (PaymentToRemove != null)
//         {
//             _appDbContext.Payments.Remove(PaymentToRemove);
//             await _appDbContext.SaveChangesAsync();
//             return true; 
//         }
//         return false; 
//     }
//     catch (Exception)
//     {
//         throw new ApplicationException("Error occurred while deleting the payment.");
//     }
// }

//     public async Task<Payment> UpdatePaymentService(Guid id, UpdatePaymentDto updatepaymentDto)
// {
//     try
//     {
//         var existingorder = await _appDbContext.Payments.FindAsync(id);

//         if (existingorder == null)
//         {
//             throw new ApplicationException("Payment not found.");
//         }

//         existingorder.Amount = updatepaymentDto.Amount;
//         existingorder.PaymentMethods = updatepaymentDto.PaymentMethods ;
       

//         _appDbContext.Payments.Update(existingorder);
//         await _appDbContext.SaveChangesAsync();

//         return existingorder;
//     }
//     catch (System.Exception)
//     {
//         throw new ApplicationException("Error occurred when updating the payment.");
//     }
// }
//     }