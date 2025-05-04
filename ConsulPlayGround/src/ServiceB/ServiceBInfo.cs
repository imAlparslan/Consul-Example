namespace ServiceB;

public static class ServiceBInfo
{
    public static string Name = "SERVICE:B";
    public static int Number { get; set; }
    public static string GetServiceInfo => $"{Name}:{Number}";

}
