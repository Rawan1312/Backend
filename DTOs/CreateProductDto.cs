public record CreateProductDto
{
  public required string Name { get; set; } = string.Empty;
  public decimal  Price { get; set; }
  
}