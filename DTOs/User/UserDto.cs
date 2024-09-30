public class UserDto
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }