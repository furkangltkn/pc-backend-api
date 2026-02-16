using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BackendServer.Services;

public class TcpServerService
{
    private TcpListener _listener;
    private TcpClient? _raspberryClient;
    private StreamWriter? _writer;
    private StreamReader? _reader;
    private Timer? _pingTimer;
    private DateTime _lastPongTime = DateTime.MinValue;

    private readonly TelemetryService _telemetryService;
    private readonly LoggingService _logger;

    public TcpServerService(TelemetryService telemetryService, LoggingService logger)
    {
        _telemetryService = telemetryService;
        _logger = logger;
    }

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, 6000);
        _listener.Start();

        _logger.Info("TCP Server başlatıldı. Raspberry bekleniyor...");
        Task.Run(AcceptClientLoop);
    }

    private async Task AcceptClientLoop()
    {
        while (true)
        {
            _raspberryClient = await _listener.AcceptTcpClientAsync();
           
            _lastPongTime = DateTime.Now;
            
            _pingTimer = new Timer(async _ =>
            {
                await SendPing();
            }, null, 0, 1000);
            _logger.Info("Raspberry Pi bağlandı.");

            var stream = _raspberryClient.GetStream();
            _reader = new StreamReader(stream, Encoding.UTF8);
            _writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            _ = Task.Run(ListenClientLoop);
        }
    }
    
    private async Task ListenClientLoop()
    {
        try
        {
            while (true)
            {
                if (_reader == null) break;

                var line = await _reader.ReadLineAsync();
                if (line == null)
                {
                    _logger.Error("Raspberry bağlantısı koptu.");
                    break;
                }

                if (line == "PONG")
                {
                    _lastPongTime = DateTime.Now;
                    continue;
                }
                
                _logger.Info("RX: " + line);
                await _telemetryService.HandleRawTelemetry(line);
            }
        }
        catch (Exception ex)
        {
            _logger.Error("TCP okuma hatası: " + ex.Message);
        }
        finally
        {
            _reader?.Close();
            _writer?.Close();
            _raspberryClient?.Close();
            _pingTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _pingTimer?.Dispose();
            
            _reader = null;
            _writer = null;
            _raspberryClient = null;
            _pingTimer = null;
            
            _logger.Info("Bağlantı temizlendi.");
        }
    }

    public async Task SendCommand(string command)
    {
        if (_raspberryClient == null || _writer == null)
        {
            _logger.Error("Raspberry bağlı değil, komut gönderilemedi.");
            return;
        }

        var msg = "CMD|" + command;
        await _writer.WriteLineAsync(msg);

        _logger.Info("TX: " + msg);
    }
    
    private async Task SendPing()
    {
        if (_writer == null) return;

        if (!await HasInternet())
        {
            _logger.Error("Internet yok -> PING atılmıyor.");
            return;
        }

        if (_lastPongTime != DateTime.MinValue && (DateTime.Now - _lastPongTime).TotalSeconds > 3)
        {
            _logger.Error("Watchdog timeout! Raspberry cevap vermiyor.");
            _raspberryClient?.Close();
            return;
        }
        
        try
        {
            await _writer.WriteLineAsync("PING");
        }
        catch
        {
            _logger.Error("PING gönderilemedi.");
        }
    }

    private async Task<bool> HasInternet()
    {
        try
        {
            using var client = new TcpClient();
            var task = client.ConnectAsync("8.8.8.8", 53);
            var timeout = Task.Delay(1000);

            var completed = await Task.WhenAny(task, timeout);
            return completed == task && client.Connected;
        }
        catch
        {
            return false;
        }
    }
    
}