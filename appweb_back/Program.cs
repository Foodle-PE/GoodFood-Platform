using appweb_back.Inventory.Application.Internal.CommandServices;
using appweb_back.Inventory.Application.Internal.QueryServices;
using appweb_back.Inventory.Domain.Repositories;
using appweb_back.Inventory.Infrastructure.Repositories;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<AddProductToInventoryCommandService>();
builder.Services.AddScoped<GetAllProductsQueryService>();


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Verify Database Connection String
try
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();
    Console.WriteLine("✅ Conexión exitosa a MySQL.");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error al conectar a MySQL: {ex.Message}");
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
