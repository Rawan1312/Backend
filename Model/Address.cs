using System.Text.Json.Serialization;

public record Address
{
  public Guid AddressId { get; set; }
  public required string City { get; set; }
  public required string State { get; set; } 
  
  public Guid UserId { get; set; } // Foreing key For the user
   [JsonIgnore]
  public User User { get; set; } // 

 
}