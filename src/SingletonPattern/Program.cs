using System;

namespace SingletonPattern;

public sealed class AppSettings
{
    private static readonly Lazy<AppSettings> _instance = new(() => new AppSettings());

    public static AppSettings Instance => _instance.Value;

    public string EnvironmentName { get; private set; } = "Development";

    private AppSettings() { }

    public void SetEnvironment(string name) => EnvironmentName = name;
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Singleton Pattern ==");

        var a = AppSettings.Instance;
        var b = AppSettings.Instance;

        Console.WriteLine($"Same instance? {ReferenceEquals(a, b)}");

        Console.WriteLine($"Before: {a.EnvironmentName}");
        b.SetEnvironment("Production");
        Console.WriteLine($"After:  {a.EnvironmentName}");

        Console.WriteLine("\nTip: Prefer DI for most shared services; Singleton is best for truly global, stateless/immutable config.");
    }
}
