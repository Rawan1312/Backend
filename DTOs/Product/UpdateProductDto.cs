using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
public class UpdateProductDto
{ [StringLength(50, ErrorMessage = "productname must be between 3 and 50 characters.", MinimumLength = 3)] 
  public string? Name { get; set; }
  
  [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
  public decimal? Price { get; set; }
    public required string Description {get;set;}
    
  public required string Author{get;set;}
  public  required string Genre {get;set;}
  public decimal PublicationYear {get;set;}
}