//Model
public class Category
{
  public Guid CategoryId { get; set; }
  public required string CategoryName { get; set; }= string.Empty;
 public required string Description{get;set;}
 public List<Product> Products =new List<Product>();

}