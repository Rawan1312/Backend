using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public record ProductDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required string CategoryName { get; set; }
      public required string  Description {get;set;}
      
  public required string Author{get;set;}
  public  required string Genre {get;set;}
  public decimal PublicationYear {get;set;}
  //  public required string ImageUrl {get;set;}

    
}