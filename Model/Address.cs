public record Address
{
  public Guid AddressId { get; set; }
  public required string City { get; set; }
  public required string State { get; set; } 
  
}