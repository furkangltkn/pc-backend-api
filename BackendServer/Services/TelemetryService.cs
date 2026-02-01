using BackendServer.Models;
using BackendServer.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace BackendServer.Services;

public class TelemetryService
{
    private readonly IHubContext<TelemetryHub> _hub;
    private readonly LoggingService _logger;

    public TelemetryService(IHubContext<TelemetryHub> hub, LoggingService logger)
    {
        _hub = hub;
        _logger = logger;
    }

    public async Task HandleRawTelemetry(string rawData)
    {
        _logger.Info("Raw Telemetry: " + rawData);

        var telemetry = ParseTelemetry(rawData);

        if (telemetry == null)
        {
            _logger.Error("Telemetry parse edilemedi.");
            return;
        }

        // UI'ya model olarak gönderiyor
        await _hub.Clients.All.SendAsync("telemetry", telemetry);
    }

    private TelemetryData? ParseTelemetry(string raw)
    {
        try
        {
            // Cihaz1|I1:23.46,I2:27.56,I3:47.32...
            var parts = raw.Split('|');
            if (parts.Length != 2) return null;

            var deviceId = parts[0];
            var payload = parts[1];

            var telemetry = new TelemetryData
            {
                DeviceId = deviceId,
                Temperature = new TemperatureData(),
                Current = new CurrentData(),
                Voltage = new VoltageData(),
                Motion = new MotionData()
            };

            var fields = payload.Split(',');

            foreach (var field in fields)
            {
                var kv = field.Split(':');
                if (kv.Length != 2) continue;

                var key = kv[0].Trim().ToUpperInvariant();
                var valueStr = kv[1];

                if (!double.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    continue;

                switch (key)
                {
                    // Temperature
                    case "BT1": telemetry.Temperature.BT1 = value; break;
                    case "BT2": telemetry.Temperature.BT2 = value; break;
                    case "BT3": telemetry.Temperature.BT3 = value; break;

                    // Current
                    case "I1": telemetry.Current.I1 = value; break;
                    case "I2": telemetry.Current.I2 = value; break;
                    case "I3": telemetry.Current.I3 = value; break;

                    // Voltage
                    case "V1": telemetry.Voltage.V1 = value; break;
                    case "V2": telemetry.Voltage.V2 = value; break;
                    case "V3": telemetry.Voltage.V3 = value; break;

                    // Motion Temperature
                    case "MT1": telemetry.Motion.MT1 = value; break;
                    case "MT2": telemetry.Motion.MT2 = value; break;
                    case "MT3": telemetry.Motion.MT3 = value; break;
                    
                    // Motion Speed
                    case "SX": telemetry.Motion.SX = value; break;
                    case "SY": telemetry.Motion.SY = value; break;
                    case "SZ": telemetry.Motion.SZ = value; break;

                    // Location
                    case "LX": telemetry.Motion.LX = value; break;
                    case "LY": telemetry.Motion.LY = value; break;
                    case "LZ": telemetry.Motion.LZ = value; break;

                    // Acceleration
                    case "AX": telemetry.Motion.AX = value; break;
                    case "AY": telemetry.Motion.AY = value; break;
                    case "AZ": telemetry.Motion.AZ = value; break;
                }
            }

            return telemetry;
        }
        catch (Exception ex)
        {
            _logger.Error("ParseTelemetry exception: " + ex.Message);
            return null;
        }
    }
}