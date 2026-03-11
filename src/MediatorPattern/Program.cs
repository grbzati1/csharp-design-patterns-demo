using System;

namespace MediatorPattern;

// Mediator interface
public interface IChatMediator
{
    void Send(string fromUser, string message);
    void Join(ChatUser user);
}

// Concrete mediator
public sealed class ChatRoom : IChatMediator
{
    private readonly List<ChatUser> _users = new();

    public void Join(ChatUser user)
    {
        _users.Add(user);
        Console.WriteLine($"[Room] {user.Name} joined.");
    }

    public void Send(string fromUser, string message)
    {
        foreach (var u in _users)
        {
            if (!string.Equals(u.Name, fromUser, StringComparison.OrdinalIgnoreCase))
                u.Receive(fromUser, message);
        }
    }
}

// Colleague base class
public abstract class ChatUser
{
    protected readonly IChatMediator Mediator;
    public string Name { get; }

    protected ChatUser(string name, IChatMediator mediator)
    {
        Name = name;
        Mediator = mediator;
        mediator.Join(this);
    }

    public void Send(string message) => Mediator.Send(Name, message);

    public abstract void Receive(string from, string message);
}

// Concrete colleagues
public sealed class StandardUser : ChatUser
{
    public StandardUser(string name, IChatMediator mediator) : base(name, mediator) { }

    public override void Receive(string from, string message) =>
        Console.WriteLine($"[{Name}] received from {from}: {message}");
}

public sealed class ModeratorUser : ChatUser
{
    public ModeratorUser(string name, IChatMediator mediator) : base(name, mediator) { }

    public override void Receive(string from, string message) =>
        Console.WriteLine($"[{Name} (mod)] received from {from}: {message}");

    public void SendAnnouncement(string message) => Send($"[ANNOUNCEMENT] {message}");
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Mediator Pattern ==");

        IChatMediator room = new ChatRoom();

        var ati = new StandardUser("Ati", room);
        var sara = new StandardUser("Sara", room);
        var mod = new ModeratorUser("Admin", room);

        ati.Send("Hi everyone!");
        sara.Send("Hello Ati 👋");
        mod.SendAnnouncement("Please keep messages short and friendly.");

        Console.WriteLine("\nMediator reduces direct dependencies between users; all coordination goes through the mediator (ChatRoom).");
    }
}
