public record OrderDetailDto
{
public required Guid OrderDetailId{get;set;}
public required decimal TotalPrice {get;set;}
 public required int Quantity {get;set;}
}