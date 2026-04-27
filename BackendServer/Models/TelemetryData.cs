namespace BackendServer.Models;

public class TelemetryData
{
    public string? DeviceId{ get; set; }
    public DateTime TimeStamp { get; set; } =  DateTime.Now;
    
    public TemperatureData? Temperature{ get; set; }
    public CurrentData? Current { get; set; }
    public VoltageData? Voltage { get; set; }
    public MotionData? Motion { get; set; }
    public PressureData? Pressure { get; set; }
    public PowerData? Power { get; set; }
}

public class TemperatureData
{
    // Battery
    public double BT1 { get; set; } // Batarya sıcaklık verileri
    public double BT2 { get; set; }
    public double BT3 { get; set; }
    public double BT4 { get; set; }
    
    // Battery temperature data packs
    public double BT5 { get; set; } // Batarya sıcaklık verileri paketi
    public double BT6 { get; set; }
    public double BT7 { get; set; }         
    public double BT8 { get; set; }
    public double BT9 { get; set; }
    public double BT10 { get; set; }
    public double BT11 { get; set; }
    public double BT12 { get; set; }
    public double BT13 { get; set; }
    public double BT14 { get; set; }
    public double BT15 { get; set; }
    public double BT16 { get; set; }
    public double BT17 { get; set; }
    public double BT18 { get; set; }
    public double BT19 { get; set; }
    public double BT20 { get; set; }
    
}

public class CurrentData
{
    // Battery current data packs
    public double I1 { get; set; } // Batarya akım verileri paketi
    public double I2 { get; set; }
    public double I3 { get; set; }
}

public class VoltageData
{
    // Battery voltage data packs
    public double V1 { get; set; } // Batarya voltaj verileri paketi
    public double V2 { get; set; }
    public double V3 { get; set; }
    public double V4 { get; set; }
    public double V5 { get; set; }
    public double V6 { get; set; }
    public double V7 { get; set; }
    public double V8 { get; set; }
    public double V9 { get; set; }
    public double V10 { get; set; }
    public double V11 { get; set; }
    public double V12 { get; set; }
    public double V13 { get; set; }
    public double V14 { get; set; }
    public double V15 { get; set; }
    public double V16 { get; set; }
    public double V17 { get; set; }
    public double V18 { get; set; }
    public double V19 { get; set; }
    public double V20 { get; set; }
    public double V21 { get; set; }
    public double V22 { get; set; }
    public double V23 { get; set; }
    public double V24 { get; set; }
    public double V25 { get; set; }
    public double V26 { get; set; }
    public double V27 { get; set; }
    public double V28 { get; set; }
    
}

public class MotionData
{
    // Motion
    public double MT1 { get; set; } // Araç üstü sıcaklık verileri
    public double MT2 { get; set; }
    public double MT3 { get; set; }
    
    // Speed
    public double SX { get; set; } // Hız verileri 
    public double SY { get; set; }
    public double SZ { get; set; }
    
    // Location
    public double LX { get; set; } // Konum verileri
    public double LY { get; set; }
    public double LZ { get; set; }
    
    // Acceleration
    public double AX { get; set; } // İvme verileri
    public double AY { get; set; }  
    public double AZ { get; set; }
    
    // Roll-Pıtch-Yaw data packs
    public double RX { get; set; } // 3d telemetri verileri 
    public double PX { get; set; }
    public double YX { get; set; }
    
    // Momentary speed-Average Speed
    public double MS { get; set; } // Anlık hız-Ortalama hız
    public double AS { get; set; }
    
    // Reflector counter
    public double RC { get; set; } // Reflektör sayacı
}

public class PressureData
{
    public double P1 { get; set; } // Kapsül fren basıncı verisi
}

public class PowerData
{
    public double PW1 { get; set; } // Güc tüketimi verileri
}
