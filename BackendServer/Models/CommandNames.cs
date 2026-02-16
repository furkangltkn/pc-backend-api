namespace BackendServer.Models;

public static class CommandNames
{
    public const string Forward = "FORWARD";
    public const string Backward = "BACKWARD";
    public const string Brake = "BRAKE";
    public const string Emergency = "EMERGENCY";
    public const string AutonomousOn = "AUTONOMOUS_ON";
    public const string AutonomousOff = "AUTONOMOUS_OFF";
    public const string Reset = "RESET";

    public static readonly HashSet<string> All = new()
    {
        Forward,
        Backward,
        Brake,
        Emergency,
        AutonomousOn,
        AutonomousOff,
        Reset
    };
}