
using appweb_back.sensors___alerts.Application.Internal.CommandServices;
using appweb_back.sensors___alerts.Application.Internal.QueryServices;
using appweb_back.sensors___alerts.Domain.Repositories;
using appweb_back.sensors___alerts.Infrastructure.Data;
using appweb_back.sensors___alerts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString)); 


//    Add CORS policy 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Inyección de dependencias
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<CreateAlertService>();
builder.Services.AddScoped<GetAlertsService>();

// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy  
app.UseCors("AllowAllPolicy");

app.UseAuthorization();
app.MapControllers();
app.Run();
