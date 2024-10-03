//Model
public class Order
{
public Guid OrderId{get;set;}
public required string NameOrder{get;set;}
//FK
public Guid CustomerId{get;set;}
 public List<OrderDetail>? OrderDetails { get; set; }

public decimal Price{get;set;}

public Guid ShipmentId {set; get;}

public Shipment Shipment {set; get;}

}