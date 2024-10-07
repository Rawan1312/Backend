public class CreatePaymentDto
{
  public required Guid PaymentId{get;set;}
  public  required decimal Amount{get;set;}
  public  PaymentMethod  PaymentMethods {get; set;} 

}