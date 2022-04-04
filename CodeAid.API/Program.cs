global using CodeAid.Shared;
global using Microsoft.EntityFrameworkCore;
using CodeAid.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var defaultConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Add AppDbContext to DI container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(defaultConnectionStr));

var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
