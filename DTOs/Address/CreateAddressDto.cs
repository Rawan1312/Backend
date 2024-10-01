using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class CreateAddressDto{
//    public Guid AddressId { get; set; }
//   public required string City { get; set; }

//   public required string State { get; set; }
// }
[Required(ErrorMessage = "City is missing.")]
        [StringLength(50, ErrorMessage = "City must be between 3 and 50 characters.", MinimumLength = 3)]
       public required string City { get; set; } 


         [Required(ErrorMessage = "State is missing.")]
        [StringLength(50, ErrorMessage  = "State must be between 3 and 50 characters.", MinimumLength = 3)]
               public required string State { get; set; }
}