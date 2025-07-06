using System.Text;
using appweb_back.iam.Application.Internal.CommandServices;
using appweb_back.iam.Application.Internal.OutboundServices;
using appweb_back.iam.Application.Internal.QueryServices;
using appweb_back.iam.Domain.Repositories;
using appweb_back.iam.Domain.Services;
using appweb_back.iam.Infrastructure.Hashing.BCrypt.Services;
using appweb_back.iam.Infrastructure.Persistence.EFC.Repositories;
using appweb_back.iam.Infrastructure.Pipeline.Middleware.Extensions;
using appweb_back.iam.Infrastructure.Tokens.JWT.Configuration;
using appweb_back.iam.Infrastructure.Tokens.Services;
using appweb_back.iam.Interfaces.ACL;
using appweb_back.iam.Interfaces.ACL.Services;
using appweb_back.Inventory.Application.Internal.CommandServices;
using appweb_back.Inventory.Application.Internal.QueryServices;
using appweb_back.Inventory.Domain.Repositories;
using appweb_back.Inventory.Infrastructure.Repositories;
using appweb_back.Profiles.Application.Internal.CommandServices;
using appweb_back.Profiles.Application.Internal.QueryService;
using appweb_back.Profiles.Domain.Repositories;
using appweb_back.Profiles.Domain.Services;
using appweb_back.Profiles.Infrastructure.Persistence.EFC.Repositories;
using appweb_back.Profiles.Interfaces.ACL;
using appweb_back.Profiles.Interfaces.ACL.Services;
using appweb_back.sensors___alerts.Application.Internal.CommandServices;
using appweb_back.sensors___alerts.Application.Internal.QueryServices;
using appweb_back.sensors___alerts.Domain.Repositories;
using appweb_back.sensors___alerts.Infrastructure.Repositories;
using appweb_back.Shared.Domain.Repositories;
using appweb_back.Shared.Infrastructure.Interfaces.ASP.Configuration;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Configuration;
using appweb_back.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ============================
// CONFIGURATION SERVICES
// ============================

// Add Controllers and Routing
builder.Services.AddControllers(options =>
    options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedAllPolicy", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



// Add EF Core Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString == null) return;
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
});

// Swagger/OpenAPI Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GoodFood API",
        Version = "v1",
        Description = "GoodFood Platform API",
        TermsOfService = new Uri("https://good-food.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "Foodle Studios",
            Email = "contact@acme.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    c.EnableAnnotations();
});
// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Profiles
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

// TokenSettings Configuration (Options pattern)
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));

// IAM / Identity
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Inventory
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<AddProductToInventoryCommandService>();
builder.Services.AddScoped<GetAllProductsQueryService>();

// Alerts
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<GetAlertsService>();
builder.Services.AddScoped<CreateAlertService>();
builder.Services.AddScoped<UpdateAlertService>();

// Configuración de autenticación JWT
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<TokenSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // true en producción
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // Puedes activarlo si defines un issuer
            ValidateAudience = false, // Puedes activarlo si defines un audience
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Ensure Database Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}
// ============================
// MIDDLEWARE PIPELINE
// ============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowedAllPolicy");

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseRequestAuthorization();
app.UseAuthorization();

app.MapControllers();
app.Run();






