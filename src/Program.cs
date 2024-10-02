using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<OrderService>();
// builder.Services.AddScoped<ShipmentService>(); 
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/",()=>"hello");

app.MapControllers();
app.Run();