using System;
using System.Collections.Generic;

namespace CommandPattern;

public interface ICommand
{
    void Execute();
    void Undo();
}

public sealed class Light
{
    public bool IsOn { get; private set; }

    public void On()
    {
        IsOn = true;
        Console.WriteLine("Light: ON");
    }

    public void Off()
    {
        IsOn = false;
        Console.WriteLine("Light: OFF");
    }
}

public sealed class LightOnCommand : ICommand
{
    private readonly Light _light;
    public LightOnCommand(Light light) => _light = light;

    public void Execute() => _light.On();
    public void Undo() => _light.Off();
}

public sealed class LightOffCommand : ICommand
{
    private readonly Light _light;
    public LightOffCommand(Light light) => _light = light;

    public void Execute() => _light.Off();
    public void Undo() => _light.On();
}

public sealed class RemoteControl
{
    private readonly Stack<ICommand> _history = new();

    public void Press(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void UndoLast()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("Nothing to undo.");
            return;
        }

        var cmd = _history.Pop();
        cmd.Undo();
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Command Pattern ==");

        var light = new Light();
        var remote = new RemoteControl();

        remote.Press(new LightOnCommand(light));
        remote.Press(new LightOffCommand(light));

        Console.WriteLine("-- Undo --");
        remote.UndoLast();
        remote.UndoLast();
        remote.UndoLast();

        Console.WriteLine("\nCommand is great for queues, macros, auditing, and undo/redo in UIs.");
    }
}
