public class CreateUserDto
    {
     
        public required string Name { get; set; } = string.Empty;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsBanned { get; set; } = false;
       

    }