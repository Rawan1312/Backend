public static class AddressValidation{

  public static bool isValidCity(string City) 
  {
    return !string.IsNullOrEmpty(City); 
  }

  public static bool isValidState(string State) 
  {
    return !string.IsNullOrEmpty(State); 
  }
}