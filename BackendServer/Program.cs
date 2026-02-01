using BackendServer.Hubs;
using BackendServer.Services;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Servisler -------------------
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddSingleton<LoggingService>();
builder.Services.AddSingleton<TelemetryService>();
builder.Services.AddSingleton<TcpServerService>();
builder.Services.AddSingleton<CommandService>();

//var logger = new LoggingService();
//logger.Info("Test log yazıldı");

// ------------------- CORS -------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // React address
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});

var app = builder.Build();

// -------------------- Middleware -------------------
app.UseRouting();
app.UseCors("AllowReact");

app.MapControllers();
app.MapHub<TelemetryHub>("/telemetry");

// -------------------- TCP Server --------------------
var tcpServer = app.Services.GetRequiredService<TcpServerService>();
tcpServer.Start();

app.Run();