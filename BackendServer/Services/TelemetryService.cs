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
                    case "BT1":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT1 = value; 
                        break;
                    case "BT2": 
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT2 = value; 
                        break;
                    case "BT3": 
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT3 = value; 
                        break;
                    case "BT4":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT4 = value;
                        break;
                    case "BT5":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT5 = value;
                        break;
                    case "BT6":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT6 = value;
                        break;
                    case "BT7":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT7 = value;
                        break;
                    case "BT8":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT8 = value;
                        break;
                    case "BT9":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT9 = value;
                        break;
                    case "BT10":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT10 = value;
                        break;
                    case "BT11":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT11 = value;
                        break;
                    case "BT12":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT12 = value;
                        break;
                    case "BT13":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT13 = value;
                        break;
                    case "BT14":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT14 = value;
                        break;
                    case "BT15":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT15 = value;
                        break;
                    case "BT16":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT16 = value;
                        break;
                    case "BT17":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT17 = value;
                        break;
                    case "BT18":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT18 = value;
                        break;
                    case "BT19":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT19 = value;
                        break;
                    case "BT20":
                        telemetry.Temperature ??= new TemperatureData();
                        telemetry.Temperature.BT20 = value;
                        break;

                    // Current
                    case "I1": 
                        telemetry.Current ??= new CurrentData();
                        telemetry.Current.I1 = value; 
                        break;
                    case "I2": 
                        telemetry.Current ??= new CurrentData();
                        telemetry.Current.I2 = value; 
                        break;
                    case "I3": 
                        telemetry.Current ??= new CurrentData();
                        telemetry.Current.I3 = value; 
                        break;

                    // Voltage
                    case "V1": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V1 = value; 
                        break;
                    case "V2": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V2 = value; 
                        break;
                    case "V3": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V3 = value; 
                        break;
                    case "V4":
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V4 = value;
                        break;
                    case "V5":
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V5 = value;
                        break;
                    case "V6": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V6 = value; 
                        break;
                    case "V7": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V7 = value; 
                        break;
                    case "V8": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V8 = value; 
                        break;
                    case "V9": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V9 = value; 
                        break;
                    case "V10": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V10 = value; 
                        break;
                    case "V11": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V11 = value; 
                        break;
                    case "V12": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V12 = value; 
                        break;
                    case "V13": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V13 = value; 
                        break;
                    case "V14": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V14 = value; 
                        break;
                    case "V15": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V15 = value; 
                        break;
                    case "V16": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V16 = value; 
                        break;
                    case "V17": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V17 = value; 
                        break;
                    case "V18": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V18 = value; 
                        break;
                    case "V19": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V19 = value; 
                        break;
                    case "V20": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V20 = value; 
                        break;
                    case "V21": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V21 = value; 
                        break;
                    case "V22": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V22 = value; 
                        break;
                    case "V23": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V23 = value; 
                        break;
                    case "V24": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V24 = value; 
                        break;
                    case "V25": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V25 = value; 
                        break;
                    case "V26": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V26 = value; 
                        break;
                    case "V27": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V27 = value; 
                        break;
                    case "V28": 
                        telemetry.Voltage ??= new VoltageData();
                        telemetry.Voltage.V28 = value; 
                        break;

                    // Motion Temperature
                    case "MT1": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.MT1 = value; 
                        break;
                    case "MT2": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.MT2 = value; 
                        break;
                    case "MT3": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.MT3 = value; 
                        break;
                    
                    // Motion Speed
                    case "SX": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.SX = value; 
                        break;
                    case "SY": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.SY = value; 
                        break;
                    case "SZ": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.SZ = value; 
                        break;

                    // Location
                    case "LX": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.LX = value; 
                        break;
                    case "LY": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.LY = value; 
                        break;
                    case "LZ": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.LZ = value; 
                        break;

                    // Acceleration
                    case "AX": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.AX = value; 
                        break;
                    case "AY": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.AY = value; 
                        break;
                    case "AZ": 
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.AZ = value; 
                        break;
                    
                    // Roll-Pıtch-Yaw
                    case "RX":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.RX = value;
                        break;
                    case "PX":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.PX = value;
                        break;
                    case "YX":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.YX = value;
                        break;
                    
                    // Momentary/Average speed
                    case "MS":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.MS = value;
                        break;
                    case "AS":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.AS = value;
                        break;
                    
                    // Reflector counter
                    case "RC":
                        telemetry.Motion ??= new MotionData();
                        telemetry.Motion.RC = value;
                        break;
                    
                    // Pressure
                    case "P1":
                        telemetry.Pressure ??= new PressureData();
                        telemetry.Pressure.P1  = value;
                        break;
                    
                    // Power
                    case "PW1":
                        telemetry.Power ??= new PowerData();
                        telemetry.Power.PW1 = value;
                        break;
                        
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