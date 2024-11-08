using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Repository.Interfaces;
using Service;
using Service.Intercaces;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<OMSdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowAngularApp");
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
