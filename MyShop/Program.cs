using Microsoft.EntityFrameworkCore;
using Resources;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserResources, UserResources>();
builder.Services.AddDbContext<ApiManageContext>(options => options.UseSqlServer
("Server=SRV2\\PUPILS;Database=API_Manage;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
