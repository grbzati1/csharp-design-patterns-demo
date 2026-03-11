using System;

namespace AbstractFactoryPattern;

// Products
public interface IButton { void Render(); }
public interface ITextBox { void Render(); }

// Concrete products (Light theme)
public sealed class LightButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Light Button");
}
public sealed class LightTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Light TextBox");
}

// Concrete products (Dark theme)
public sealed class DarkButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Dark Button");
}
public sealed class DarkTextBox : ITextBox
{
    public void Render() => Console.WriteLine("Rendering Dark TextBox");
}

// Abstract Factory
public interface IUiFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}

// Concrete factories
public sealed class LightUiFactory : IUiFactory
{
    public IButton CreateButton() => new LightButton();
    public ITextBox CreateTextBox() => new LightTextBox();
}

public sealed class DarkUiFactory : IUiFactory
{
    public IButton CreateButton() => new DarkButton();
    public ITextBox CreateTextBox() => new DarkTextBox();
}

// Client
public sealed class Screen
{
    private readonly IButton _button;
    private readonly ITextBox _textBox;

    public Screen(IUiFactory factory)
    {
        _button = factory.CreateButton();
        _textBox = factory.CreateTextBox();
    }

    public void Render()
    {
        _button.Render();
        _textBox.Render();
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Abstract Factory Pattern ==");

        IUiFactory factory = DateTime.UtcNow.Hour < 12
            ? new LightUiFactory()
            : new DarkUiFactory();

        var screen = new Screen(factory);
        screen.Render();

        Console.WriteLine("\nAbstract Factory is great when you need consistent families of objects (e.g., themes, providers, cloud vendors).");
    }
}
