namespace ServiceC;

public static class ServiceCInfo
{
    public static string Name = "SERVICE:C";
    public static int Number { get; set; }
    public static string GetServiceInfo => $"{Name}:{Number}";

}
