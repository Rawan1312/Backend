
public class  Product
{
  public Guid Id { get; set; }
  public string Name { get; set; }= string.Empty;
  public decimal Price { get; set; }
  public DateTime CreatedAt { get; set; }
  public Guid CategoryId { get; set; }
  public Category Category{ get; set; }

}