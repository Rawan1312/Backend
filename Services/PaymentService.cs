using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

public interface IPaymentService{
    Task<List<Payment>> GetAllPaymentService();
    Task<Payment> CreatePaymentService(CreatePaymentDto newpayment);
    Task<PaymentDto?> GetPaymentByIdService(Guid paymentId);
    Task<bool> DeletePaymentByIdService(Guid id);
     Task<Payment> UpdatePaymentService(Guid id, UpdatePaymentDto updatepaymentDto);
}
public class PaymentService :IPaymentService 
{
    private readonly AppDBContext _appDbContext;
    private readonly IMapper _mapper;

    public PaymentService(AppDBContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper; }
      public async Task<PaymentDto?> GetPaymentByIdService(Guid PaymentId)
{
     try
    {
        var payment = await _appDbContext.Payments
            .FirstOrDefaultAsync(p => p.PaymentId == PaymentId);

        if (payment == null)
        {
            return null; // Return null if user not found
        }
          return _mapper.Map<PaymentDto>(payment);
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the user.");
    }
}

    public async Task<List<Payment>> GetAllPaymentService()
    {
      try
      {
        var payment= await _appDbContext.Payments.Include(u => u.User).ToListAsync();
      return payment;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the payment table");
      }
    }
      // Create a new payment
    public async Task<Payment> CreatePaymentService(CreatePaymentDto newpayment)
    {
        try
        {
           var payment = _mapper.Map<Payment>(newpayment);
         await _appDbContext.Payments.AddAsync(payment);
         await _appDbContext.SaveChangesAsync();
         return payment;
         
         }  
        catch (Exception)
        {
            throw new ApplicationException("Error occurred when creating the Payment.");
        } }  
    public async Task<bool> DeletePaymentByIdService(Guid id)
{
    try
    {
        var PaymentToRemove = await _appDbContext.Payments.FirstOrDefaultAsync(d => d.PaymentId == id);

        if (PaymentToRemove != null)
        {
            _appDbContext.Payments.Remove(PaymentToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the payment.");
    }
}

    public async Task<Payment> UpdatePaymentService(Guid id, UpdatePaymentDto updatepaymentDto)
{
    try
    {
        var existinPayment = await _appDbContext.Payments.FindAsync(id);

        if (existinPayment == null)
        {
            throw new ApplicationException("Payment not found."); }
             _mapper.Map(updatepaymentDto, existinPayment);

        _appDbContext.Payments.Update(existinPayment);
        await _appDbContext.SaveChangesAsync();

        return existinPayment;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the payment.");
    }
}
}