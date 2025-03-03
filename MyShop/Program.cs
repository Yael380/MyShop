//using Microsoft.EntityFrameworkCore;
//using MyShop;
//using NLog.Web;
//using Resources;
//using Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddScoped<IUserServices, UserServices>();
//builder.Services.AddScoped<IUserResources, UserResources>();

//builder.Services.AddScoped<IProductServices, ProductServices>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();

//builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddScoped<ICategoryServices, CategoryServices>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//builder.Services.AddScoped<IRatingServices, RatingServices>();
//builder.Services.AddScoped<IRatingRepository, RatingRepository>();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<ApiManageContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection")));
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Host.UseNLog();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.UseRatingMiddleware();
//app.UseErrorHandlingMiddleware();

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseAuthorization();


//app.MapControllers();

//app.Run();

using Microsoft.EntityFrameworkCore;
using MyShop;
using NLog.Web;
using Repository;
using Services;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IRatingServices, RatingServices>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ApiManageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Memory Cache services
builder.Services.AddMemoryCache();

// Add Response Caching Middleware services
//builder.Services.AddResponseCaching();

builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use Response Caching Middleware
//app.UseResponseCaching();

app.UseRatingMiddleware();
app.UseErrorHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();