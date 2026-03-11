using System;

namespace StrategyPattern;

public interface IDiscountStrategy
{
    decimal Apply(decimal subtotal);
}

public sealed class NoDiscount : IDiscountStrategy
{
    public decimal Apply(decimal subtotal) => subtotal;
}

public sealed class PercentageDiscount : IDiscountStrategy
{
    private readonly decimal _percent; // 0.10m = 10%
    public PercentageDiscount(decimal percent) => _percent = percent;

    public decimal Apply(decimal subtotal) => subtotal - (subtotal * _percent);
}

public sealed class ThresholdDiscount : IDiscountStrategy
{
    private readonly decimal _threshold;
    private readonly decimal _amountOff;

    public ThresholdDiscount(decimal threshold, decimal amountOff)
    {
        _threshold = threshold;
        _amountOff = amountOff;
    }

    public decimal Apply(decimal subtotal) => subtotal >= _threshold ? subtotal - _amountOff : subtotal;
}

public sealed class Checkout
{
    private IDiscountStrategy _strategy;

    public Checkout(IDiscountStrategy strategy) => _strategy = strategy;

    public void SetStrategy(IDiscountStrategy strategy) => _strategy = strategy;

    public decimal Total(decimal subtotal) => _strategy.Apply(subtotal);
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Strategy Pattern ==");

        var checkout = new Checkout(new NoDiscount());

        var subtotal = 120m;
        Console.WriteLine($"Subtotal: {subtotal:C}");

        checkout.SetStrategy(new PercentageDiscount(0.10m));
        Console.WriteLine($"10% off:  {checkout.Total(subtotal):C}");

        checkout.SetStrategy(new ThresholdDiscount(threshold: 100m, amountOff: 15m));
        Console.WriteLine($"£15 off:  {checkout.Total(subtotal):C}");

        Console.WriteLine("\nStrategy is ideal for pricing/rules engines (you can add strategies without rewriting Checkout).");
    }
}
