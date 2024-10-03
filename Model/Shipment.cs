public record Shipment
{
  public Guid ShipmentId { get; set; }
  public  DateTime ShipmentDate { get; set; }= DateTime.UtcNow;
    public string CompanyName { get; internal set; }

    public Guid OrderId { get; set; } // Foreing key For the user
  public Order Order { get; set; } // 
}