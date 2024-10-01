public record ShipmentDto
{
  public required String CompanyName { get; set; } 
  public Guid ShipmentId { get; set; }
  public  DateTime ShipmentDate { get; set; }= DateTime.UtcNow;
  
}