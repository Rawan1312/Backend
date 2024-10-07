

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
public class UpdateAddress
{
 
 [StringLength(50, ErrorMessage = "City must be between 3 and 50 characters.", MinimumLength = 3)]
 public required string City { get; set; } = string.Empty;

[StringLength(50, ErrorMessage = "State must between 3 and 50 characters.",MinimumLength = 3)]
   public string? State { get; set; }
  
}