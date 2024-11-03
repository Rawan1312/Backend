using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
public record CreateProductDto
{
  [Required(ErrorMessage = "productname is missing.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
  public required string Name { get; set; } = string.Empty;
  
  [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
  public decimal  Price { get; set; }
  public Guid CategoryId { get; set; }
    public required string description {get;set;}
  public required string genre {get;set;}
  public decimal PublicationYear {get;set;}

  
}