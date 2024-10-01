public static class ShipmentValidation
{
  public static bool isValidName(string CompanyName) 
  {
    return !string.IsNullOrEmpty(CompanyName); 
  }
  
  }
  