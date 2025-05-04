namespace ServiceA;

public static class ServiceAInfo
{
    public static string Name => "SERVICE:A";
    public static int Number { get; set; }
    public static string GetServiceInfo => $"{Name}:{Number}";
}
