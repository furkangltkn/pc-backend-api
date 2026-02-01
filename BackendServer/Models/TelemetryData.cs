namespace BackendServer.Models;

public class TelemetryData
{
    public string? DeviceId{ get; set; }
    public DateTime TimeStamp { get; set; } =  DateTime.Now;
    
    public TemperatureData Temperature{ get; set; }
    public CurrentData Current { get; set; }
    public VoltageData Voltage { get; set; }
    public MotionData Motion { get; set; }
}

public class TemperatureData
{
    // Battery
    public double BT1 { get; set; } // Batarya sıcaklık verileri
    public double BT2 { get; set; }
    public double BT3 { get; set; }
}

public class CurrentData
{
    // Current
    public double I1 { get; set; } // Akım verileri
    public double I2 { get; set; }
    public double I3 { get; set; }
}

public class VoltageData
{
    // Voltage
    public double V1 { get; set; } // Voltaj verileri
    public double V2 { get; set; }
    public double V3 { get; set; }
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
    
}
    
