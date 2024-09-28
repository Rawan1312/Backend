using Microsoft.EntityFrameworkCore;

public class AppDBContext:DbContext{
public AppDBContext(DbContextOptions<AppDBContext> options):base(options){}
public DbSet<User> Users {get; set;}
public DbSet<Product> Product {get; set;}
public DbSet<CategoryDto> Category {get; set;}
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

          modelBuilder.Entity<CategoryDto>(entity=>{
          entity.HasKey(e=>e.CategoryId);
          entity.Property(e=>e.CategoryId).HasDefaultValueSql("uuid_generate_v4()");
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Description);
          });
    }

}