public record OrderDto
{
public required Guid OrderId{get;set;}
public required string NameOrder{get;set;}
public required decimal Price{get;set;}
}
