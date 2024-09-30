public static class CategoryValidation{

  public static bool isValidName(string name) 
  {
    return !string.IsNullOrEmpty(name); 
  }
}