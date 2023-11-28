using System.Text.Json.Serialization;
using inventory.Authorization;
using inventory.Helpers;
using inventory.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


    services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);


    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IProductKeluarService, ProductKeluarService>();
    services.AddScoped<IProductMasukService, ProductMasukService>();
    services.AddScoped<ISaleService, SaleService>();
    services.AddScoped<ISupplierService, SupplierService>();
}




var app = builder.Build();



{

    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());


    app.UseMiddleware<ErrorHandlerMiddleware>();


    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run();

