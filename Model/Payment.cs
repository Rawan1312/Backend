//Model
public class Payment
{
  public Guid PaymentId{get;set;}
  public decimal Amount{get;set;}

// public Payment PaymentMethod {get; set;}
  public  PaymentMethod  PaymentMethods {get; set;} 
public Guid UserId { get; set; }
  public User User{ get; set; }
}
