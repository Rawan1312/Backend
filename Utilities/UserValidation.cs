public static class UserValidation{
public static bool isValidName(string name) 
  {
    return !string.IsNullOrEmpty(name); 
  }
  public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

            if (!email.Contains("@"))
            return false;

      
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
}
}