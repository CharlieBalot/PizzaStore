using Microsoft.EntityFrameworkCore;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaAPI.Services;
using PizzaData.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Interfaces Scope services
builder.Services.AddScoped<IRepository<Pizzas>, PizzaService>();
builder.Services.AddScoped<IRepository<PizzaType>, PizzaTypeService>();
builder.Services.AddScoped<IRepository<Orders>, OrdersService>();
builder.Services.AddScoped<IRepository<OrderDetails>, OrderDetailsService>();

//Connect to SQL Server Database
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
