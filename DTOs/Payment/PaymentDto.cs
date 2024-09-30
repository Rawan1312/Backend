public class PaymentDto
{
  public required Guid PaymentId{get;set;}
  public  required decimal Amount{get;set;}
  public enum PaymentMethod;
}