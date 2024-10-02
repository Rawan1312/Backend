public record Shipment
{
  public Guid ShipmentId { get; set; }
  public  DateTime ShipmentDate { get; set; }= DateTime.UtcNow;
  
}