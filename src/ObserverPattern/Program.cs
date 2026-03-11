using System;
using System.Collections.Generic;

namespace ObserverPattern;

public interface IObserver<in T>
{
    void OnNext(T value);
}

public interface ISubject<T>
{
    IDisposable Subscribe(IObserver<T> observer);
}

public sealed class TemperatureStation : ISubject<int>
{
    private readonly List<IObserver<int>> _observers = new();

    public IDisposable Subscribe(IObserver<int> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }

    public void SetTemperature(int celsius)
    {
        Console.WriteLine($"[Station] Temperature changed to {celsius}°C");
        foreach (var o in _observers)
            o.OnNext(celsius);
    }

    private sealed class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<int>> _list;
        private readonly IObserver<int> _observer;

        public Unsubscriber(List<IObserver<int>> list, IObserver<int> observer)
        {
            _list = list;
            _observer = observer;
        }

        public void Dispose() => _list.Remove(_observer);
    }
}

public sealed class PhoneDisplay : IObserver<int>
{
    public void OnNext(int value) =>
        Console.WriteLine($"[Phone]  Now showing {value}°C");
}

public sealed class WallDisplay : IObserver<int>
{
    public void OnNext(int value) =>
        Console.WriteLine($"[Wall]   Big display shows {value}°C");
}

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("== Observer Pattern ==");

        var station = new TemperatureStation();

        var phone = new PhoneDisplay();
        var wall = new WallDisplay();

        using var sub1 = station.Subscribe(phone);
        using var sub2 = station.Subscribe(wall);

        station.SetTemperature(19);
        station.SetTemperature(21);

        Console.WriteLine("\nObserver is great for domain events, UI events, reactive streams, and pub/sub style updates.");
    }
}
