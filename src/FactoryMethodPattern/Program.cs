using System;

namespace FactoryMethodPattern;

public interface INotification
{
    void Send(string to, string message);
}

public sealed class EmailNotification : INotification
{
    public void Send(string to, string message) =>
        Console.WriteLine($"[EMAIL] To={to} :: {message}");
}

public sealed class SmsNotification : INotification
{
    public void Send(string to, string message) =>
        Console.WriteLine($"[SMS]   To={to} :: {message}");
}

public abstract class NotificationCreator
{
    // Factory Method: subclasses decide which product to instantiate.
    public abstract INotification Create();

    public void Notify(string to, string message)
    {
        var channel = Create();
        channel.Send(to, message);
    }
}

public sealed class EmailNotificationCreator : NotificationCreator
{
    public override INotification Create() => new EmailNotification();
}

public sealed class SmsNotificationCreator : NotificationCreator
{
    public override INotification Create() => new SmsNotification();
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Factory Method Pattern ==");

        NotificationCreator creator = DateTime.UtcNow.Second % 2 == 0
            ? new EmailNotificationCreator()
            : new SmsNotificationCreator();

        creator.Notify("test@example.com", "Hello from Factory Method!");
    }
}
