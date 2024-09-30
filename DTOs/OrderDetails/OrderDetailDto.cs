public record OrderDetailDto
{
public required Guid OrderDetailId{get;set;}
public required decimal Total_Price {get;set;}
 public required int Quantity {get;set;}
}