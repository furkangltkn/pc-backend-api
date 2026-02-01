using System.IO;

namespace BackendServer.Services;

public class LoggingService
{
    private readonly string logFilePath;
    private readonly object _lock = new object();

    public LoggingService()
    {
        var basePath = AppContext.BaseDirectory; // Uygulamanın çalıştığı dizin
        var logDirectory = Path.Combine(basePath, "Logs");

        Directory.CreateDirectory(logDirectory); // Yoksa oluşturur
        logFilePath = Path.Combine(logDirectory, "app.log");
    }
    public void Info(string message)
    {
        WriteLog("INFO", message);
    }

    public void Error(string message)
    {
        WriteLog("ERROR", message);
    }

    public void WriteLog(string level, string message)
    {
        var log = $"[{level}] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

        lock (_lock)
        {
            Console.WriteLine(log);
            File.AppendAllText(logFilePath, log + Environment.NewLine);
        }
    }
}

