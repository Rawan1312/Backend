using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
public class CreateCategoryDto{
  public Guid CategoryId { get; set; }
  
  [Required(ErrorMessage = "categoryname is missing.")]
  [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
  public required string CategoryName { get; set; }
}