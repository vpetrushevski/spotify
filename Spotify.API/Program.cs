
using Serilog.Events;
using Serilog;
using Spotify.API.Config;
using Spotify.API.Extensions;
using Spotify.Application.Extensions;
using Spotify.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Initialize AppConfig.cs
var appConfig = new AppConfig(builder);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddFluentValidators();

#region Logging Setup

var logPath = AppConfig.Configuration.GetSection("LogPath")?.Value ?? ".\\Logs";

Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(Path.Combine(logPath, "log-.txt"), LogEventLevel.Verbose, rollingInterval: RollingInterval.Day)
                    .WriteTo.File(Path.Combine(logPath, "log-error-.txt"), LogEventLevel.Error, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 100)
                    .Enrich.FromLogContext()
                    .CreateLogger();
#endregion

// Configure controllers
builder.Services.ConfigureControllers();

// Configure DB Context as an DI service
//builder.Services.ConfigureDbContext(AppConfig.Configuration.GetConnectionString("Main"));

// Cross-Origin Resource Sharing (CORS)
builder.Services.ConfigureCors();

// Register required repositories
//builder.Services.AddRepositories();

// Register required services
builder.Services.AddServices(AppConfig.Configuration);

// Configure Auto Mapper
builder.Services.ConfigureAutoMapper();

// Service responsible for returning validation errors using Data annotation validation approach
builder.Services.ConfigureValidationResponse();

#region Session

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(config =>
{
    config.Cookie.Name = "Brosurance.Session";
    config.IdleTimeout = TimeSpan.FromHours(24);
});

#endregion

var app = builder.Build();

app.ConfigureSession();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Global error handling
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

// CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
