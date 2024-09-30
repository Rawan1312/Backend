public record ShipmentDto
{
  public Guid ShipmentId { get; set; }
  public  DateTime ShipmentDate { get; set; }= DateTime.UtcNow;
  
}