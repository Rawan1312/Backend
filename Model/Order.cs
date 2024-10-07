//Model
public class Order
{
public Guid OrderId{get;set;}
public required string NameOrder{get;set;}
 public List<OrderDetail>? OrderDetails { get; set; }

public decimal Price{get;set;}
public Guid ShipmentId {set; get;}
public Shipment Shipment {set; get;}
public Guid UserId { get; set; }
  public User User{ get; set; }

}