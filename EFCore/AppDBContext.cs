using Microsoft.EntityFrameworkCore;
public class AppDBContext:DbContext{
public AppDBContext(DbContextOptions<AppDBContext> options):base(options){}
public DbSet<User> Users {get; set;}
public DbSet<Product> Product {get; set;}
public DbSet<Category> Category {get; set;}
public DbSet<Order> Orders{get;set;}
public DbSet<OrderDetail> OrderDetails {get;set;}
public DbSet<Payment> Payments {get;set;}
public DbSet<Address> Address {get; set;}
public DbSet<Shipment> Shipment {get; set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
        modelBuilder.Entity<User>(entity=>{
          entity.HasKey(e=>e.UserId);
          entity.Property(e=>e.UserId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.HasIndex(e => e.Name).IsUnique();
          entity.Property(e=>e.Email).IsRequired(); 
          entity.Property(e=>e.Password).IsRequired();
          entity.Property(e=>e.IsAdmin);
          entity.Property(e=>e.IsBanned);
          entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
          });
          modelBuilder.Entity<User>()
            .HasMany(o => o.Order)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany(p => p.Payment)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
            .HasMany(adr => adr.Address)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

          modelBuilder.Entity<Product>(entity=>{
          entity.HasKey(e=>e.Id);
          entity.Property(e=>e.Id).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.HasIndex(e => e.Name).IsUnique();
          entity.Property(e=>e.Price).IsRequired();
          entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
          });
          modelBuilder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            // when you delete the category must be delete the product auto
            .OnDelete(DeleteBehavior.Cascade);
            
            
            
          modelBuilder.Entity<Category>(entity=>{
          entity.HasKey(e=>e.CategoryId);
          entity.Property(e=>e.CategoryId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.CategoryName).IsRequired().HasMaxLength(200);
          entity.HasIndex(e => e.CategoryName).IsUnique();
          entity.Property(e=>e.Description);});
          
          modelBuilder.Entity<Order>(entity=>{
          entity.HasKey(e=>e.OrderId);
          entity.Property(e=>e.OrderId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.NameOrder).IsRequired().HasMaxLength(200);
          entity.HasIndex(e => e.NameOrder).IsUnique();
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
          entity.Property(e => e.PaymentMethods).IsRequired() .HasConversion<string>();
            });
          

           modelBuilder.Entity<Address>(entity=>{
          entity.HasKey(e=>e.AddressId);
          entity.Property(e=>e.AddressId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.City).IsRequired().HasMaxLength(100);
          entity.Property(e=>e.State).IsRequired();
        
          });

             
        

          modelBuilder.Entity<Shipment>(entity=>{
          entity.HasKey(e=>e.ShipmentId);
          entity.Property(e=>e.ShipmentId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.CompanyName).IsRequired().HasMaxLength(100);
          entity.Property(e=>e.ShipmentDate).IsRequired().HasMaxLength(100);

        
          });

          modelBuilder.Entity<Order>()
          .HasOne<Shipment>(sh => sh.Shipment)
          .WithOne(o => o.Order)
          .HasForeignKey<Shipment>(o => o.OrderId)
          .OnDelete(DeleteBehavior.Cascade);
    
        // ex:one-to-one
            // modelBuilder.Entity<User>()
            // .HasOne(c => c.Profile)
            // .WithOne(p => p.user)
            // .HasForeignKey(p => p.UserId)
            // // when you delete the category must be delete the product auto
            // .OnDelete(DeleteBehavior.Cascade);

    }

}