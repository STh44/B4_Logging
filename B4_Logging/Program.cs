using Serilog;

var builder = WebApplication.CreateBuilder(args);

// SeriLog konfigurieren und aus appsettings.json laden
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Dienste hinzufügen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger und Swagger UI einrichten
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware und Routen einrichten
app.UseSerilogRequestLogging(); // Protokolliert HTTP-Anfragen
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Log.Information("Die Anwendung wurde gestartet.");
app.Run();









