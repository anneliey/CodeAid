global using CodeAid.Shared;
global using Microsoft.EntityFrameworkCore;
using CodeAid.API.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var defaultConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Add AppDbContext to DI container
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(defaultConnectionStr));

var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

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
