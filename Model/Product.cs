
public class  Product
{
  public Guid Id { get; set; }
  public string Name { get; set; }= string.Empty;
  public decimal Price { get; set; }
  public DateTime CreatedAt { get; set; }
  public Guid CategoryId { get; set; }
  public Category Category{ get; set; }
  public  required string Description {get;set;}
  public required string Genre {get;set;}
  public required string Author{get;set;}
  public decimal PublicationYear {get;set;}

  public  string ImageUrl {get;set;} =string.Empty;


}