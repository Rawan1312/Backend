public class CreateOrderDto
{
 public Guid OrderId{get;set;}
public required string NameOrder{get;set;}

  public required decimal Price{get;set;}
  public Guid UserId { get; set; }
}
