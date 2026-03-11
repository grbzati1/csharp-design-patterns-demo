# CSharpDesignPatternsDemo

A practical **C#/.NET** solution that demonstrates major **design patterns** using separate **Console Applications**.

## Included patterns (each is its own console app)

1. **Singleton** — one shared instance across the app lifetime.
2. **Factory Method** — create objects via a factory method without binding to concrete types.
3. **Abstract Factory** — create families of related objects.
4. **Strategy** — swap algorithms at runtime.
5. **Observer** — publish/subscribe notifications.
6. **Decorator** — add behavior without changing the original type.
7. **Adapter** — make incompatible interfaces work together.
8. **Command** — encapsulate actions as objects (undo/redo-friendly).
9. **Mediator** — coordinate interactions between objects to reduce coupling.

## Prerequisites

- .NET SDK (recommended: **.NET 8**)

## How to run

From the repo root:

```bash
dotnet build
```

Run any pattern demo, for example:

```bash
dotnet run --project src/StrategyPattern/StrategyPattern.csproj
```

Or list projects:

```bash
dotnet sln list
```

## Folder structure

- `src/<PatternName>Pattern/` — each pattern's console project
- `README.md` — this file

## Notes

These demos are intentionally small and readable:
- Each project has a single `Program.cs` with the key types kept close together.
- The goal is to show the **shape** of the pattern and when it’s useful.

Happy coding!
