using Microsoft.EntityFrameworkCore;
public class AppDBContext:DbContext{
public AppDBContext(DbContextOptions<AppDBContext> options):base(options){}
public DbSet<User> Users {get; set;}
public DbSet<Product> Product {get; set;}
public DbSet<Category> Category {get; set;}
public DbSet<Order> Orders{get;set;}
public DbSet<OrderDetail> OrderDetails {get;set;}
public DbSet<Payment> payments {get;set;}
public DbSet<CategoryDto> category {get; set;}
public DbSet<AddressDto> Address {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity=>{
          entity.HasKey(e=>e.UserId);
          entity.Property(e=>e.UserId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Email).IsRequired(); 
        //   entity.Property(e=>e.Email).IsUnique();
          entity.Property(e=>e.Password).IsRequired();
          entity.Property(e=>e.IsAdmin);
          entity.Property(e=>e.IsBanned);
          entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
          });
          modelBuilder.Entity<Product>(entity=>{
          entity.HasKey(e=>e.Id);
          entity.Property(e=>e.Id).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Price).IsRequired();
          entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
          });
          modelBuilder.Entity<Category>(entity=>{
          entity.HasKey(e=>e.CategoryId);
          entity.Property(e=>e.CategoryId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.CategoryName).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Description);
          
          modelBuilder.Entity<Order>(entity=>{
          entity.HasKey(e=>e.OrderId);
          entity.Property(e=>e.OrderId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.NameOrder).IsRequired().HasMaxLength(200);
          // entity.HasMany(e=>e.OrderDetails).WithOne();
          entity.Property(e=>e.Price);
          });
          modelBuilder.Entity<OrderDetail>(entity=>{
          entity.HasKey(e=>e.OrderDetailId);
          entity.Property(e=>e.OrderDetailId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.TotalPrice).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Quantity);
            });
          modelBuilder.Entity<Payment>(entity=>{
          entity.HasKey(e=>e.PaymentId);
          entity.Property(e=>e.PaymentId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Amount).IsRequired().HasColumnType("decimal(29,18)");
          entity.Property(e => e.PaymentMethod).IsRequired() .HasConversion<string>();
            });
          });

           modelBuilder.Entity<AddressDto>(entity=>{
          entity.HasKey(e=>e.AddressId);
          entity.Property(e=>e.AddressId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.City).IsRequired().HasMaxLength(100);
          entity.Property(e=>e.State).IsRequired();
        
          });
    }

}