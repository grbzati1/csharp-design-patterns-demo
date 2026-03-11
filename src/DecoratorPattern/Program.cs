using System;

namespace DecoratorPattern;

public interface ICoffee
{
    string Description { get; }
    decimal Cost();
}

public sealed class Espresso : ICoffee
{
    public string Description => "Espresso";
    public decimal Cost() => 2.00m;
}

public abstract class CoffeeDecorator : ICoffee
{
    private readonly ICoffee _inner;

    protected CoffeeDecorator(ICoffee inner) => _inner = inner;

    public virtual string Description => _inner.Description;
    public virtual decimal Cost() => _inner.Cost();
}

public sealed class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee inner) : base(inner) { }

    public override string Description => base.Description + ", Milk";
    public override decimal Cost() => base.Cost() + 0.50m;
}

public sealed class VanillaDecorator : CoffeeDecorator
{
    public VanillaDecorator(ICoffee inner) : base(inner) { }

    public override string Description => base.Description + ", Vanilla";
    public override decimal Cost() => base.Cost() + 0.35m;
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Decorator Pattern ==");

        ICoffee coffee = new Espresso();
        coffee = new MilkDecorator(coffee);
        coffee = new VanillaDecorator(coffee);

        Console.WriteLine($"{coffee.Description} => {coffee.Cost():C}");

        Console.WriteLine("\nDecorator is useful for cross-cutting behavior (caching, logging, retries) without inheritance explosion.");
    }
}
