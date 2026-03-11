using System;

namespace AdapterPattern;

// Target interface your app expects
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}

// Adaptee (legacy / 3rd party)
public sealed class LegacyPaymentsSdk
{
    public void MakePaymentInPence(int amountInPence) =>
        Console.WriteLine($"[LegacySDK] Payment executed: {amountInPence} pence");
}

// Adapter
public sealed class LegacyPaymentsAdapter : IPaymentProcessor
{
    private readonly LegacyPaymentsSdk _sdk;

    public LegacyPaymentsAdapter(LegacyPaymentsSdk sdk) => _sdk = sdk;

    public void Pay(decimal amount)
    {
        // Convert pounds to pence (simple demo conversion).
        var pence = (int)Math.Round(amount * 100m, MidpointRounding.AwayFromZero);
        _sdk.MakePaymentInPence(pence);
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Adapter Pattern ==");

        IPaymentProcessor processor = new LegacyPaymentsAdapter(new LegacyPaymentsSdk());
        processor.Pay(12.34m);

        Console.WriteLine("\nAdapter is a go-to when integrating older libraries/APIs without rewriting the rest of your code.");
    }
}
