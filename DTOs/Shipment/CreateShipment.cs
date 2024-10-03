// public record CreateShipment
// {

//   public required String CompanyName { get; set; } 
//   public required DateTime shipmentDate { get; set; } 
  
// 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class CreateShipment{
//    public Guid AddressId { get; set; }
//   public required string City { get; set; }

//   public required string State { get; set; }
// }
[Required(ErrorMessage = "CompanyName is missing.")]
        [StringLength(50, ErrorMessage = "CompanyName must be between 3 and 50 characters.", MinimumLength = 3)]
       public required string CompanyName { get; set; } 


         [Required(ErrorMessage = "shipmentDate is Invalid.")]
        [Range(typeof(DateTime), "01/01/2024", "12/31/2100", ErrorMessage = "Date must be between 01/01/2024 and 12/31/2100")]
               public required string shipmentDate { get; set; }
}