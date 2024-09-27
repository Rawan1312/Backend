using Microsoft.EntityFrameworkCore;

public class AppDBContext:DbContext{
public AppDBContext(DbContextOptions<AppDBContext> options):base(options){}
public DbSet<UserDto> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDto>(entity=>{
          entity.HasKey(e=>e.UserId);
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Email).IsRequired(); 
          entity.Property(e=>e.Password).IsRequired();
          entity.Property(e=>e.IsAdmin);
          entity.Property(e=>e.IsBanned);
         entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //  //modelBuilder.Entity<UserDto>()
        // .Property(u => u.CreatedAt)
        // .HasDefaultValueSql("CURRENT_TIMESTAMP");
           });

           modelBuilder.Entity<Product>(entity=>{
          entity.HasKey(e=>e.Id);
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Price).IsRequired();
          entity.Property(e=>e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
          });

          modelBuilder.Entity<CategoryDto>(entity=>{
          entity.HasKey(e=>e.CategoryId);
          entity.Property(e=>e.Name).IsRequired().HasMaxLength(200);
          entity.Property(e=>e.Description);
          });
    }

}