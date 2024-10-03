//Model

public class User
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
         public List<Order> Order =new List<Order>();
         public List<Payment> Payment =new List<Payment>();
         public List<Address> Address =new List<Address>();


    }