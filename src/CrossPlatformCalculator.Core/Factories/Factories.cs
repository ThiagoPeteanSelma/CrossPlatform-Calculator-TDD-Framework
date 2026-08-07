using CrossPlatformCalculator.Core.Models;
using CrossPlatformCalculator.Core.Operations;

namespace CrossPlatformCalculator.Core.Factories;

public abstract class OperationFactory
{
    public abstract ICalculationOperation CreateOperation(string symbol);

    public abstract IReadOnlyCollection<OperationDefinition> GetAvailableOperations();
}

public sealed class DefaultOperationFactory : OperationFactory
{
    private static readonly IReadOnlyDictionary<string, Func<ICalculationOperation>> OperationMap =
        new Dictionary<string, Func<ICalculationOperation>>(StringComparer.Ordinal)
        {
            ["+"] = () => new AdditionOperation(),
            ["-"] = () => new SubtractionOperation(),
            ["×"] = () => new MultiplicationOperation(),
            ["*"] = () => new MultiplicationOperation(),
            ["÷"] = () => new DivisionOperation(),
            ["/"] = () => new DivisionOperation(),
            ["%"] = () => new ModuloOperation(),
            ["√"] = () => new SquareRootOperation(),
            ["sqrt"] = () => new SquareRootOperation(),
            ["x²"] = () => new SquareOperation(),
            ["square"] = () => new SquareOperation(),
            ["1/x"] = () => new ReciprocalOperation(),
            ["reciprocal"] = () => new ReciprocalOperation()
        };

    private static readonly IReadOnlyCollection<OperationDefinition> AvailableOperations =
        [
            new("+", "Addition", true),
            new("-", "Subtraction", true),
            new("×", "Multiplication", true),
            new("÷", "Division", true),
            new("%", "Modulo", true),
            new("√", "Square Root", false),
            new("x²", "Square", false),
            new("1/x", "Reciprocal", false)
        ];

    public override ICalculationOperation CreateOperation(string symbol)
    {
        var normalizedSymbol = NormalizeSymbol(symbol);

        if (OperationMap.TryGetValue(normalizedSymbol, out var factory))
        {
            return factory();
        }

        throw new NotSupportedException($"Operation '{symbol}' is not supported.");
    }

    public override IReadOnlyCollection<OperationDefinition> GetAvailableOperations() => AvailableOperations;

    private static string NormalizeSymbol(string symbol)
    {
        var normalized = symbol.Trim();
        return normalized.All(char.IsLetter) ? normalized.ToLowerInvariant() : normalized;
    }
}

public interface IPlatformUiFactory
{
    string Platform { get; }

    IReadOnlyDictionary<string, string> CreateIcons();

    PlatformLayout CreateLayout();
}

public sealed class WebUiFactory : IPlatformUiFactory
{
    public string Platform => "web";

    public IReadOnlyDictionary<string, string> CreateIcons() => new Dictionary<string, string>
    {
        ["+"] = "icon-plus-web",
        ["-"] = "icon-minus-web",
        ["="] = "icon-equals-web"
    };

    public PlatformLayout CreateLayout() => new("grid-web", "grid", ["7", "8", "9", "+", "=", "√"]);
}

public sealed class MobileUiFactory : IPlatformUiFactory
{
    public string Platform => "mobile";

    public IReadOnlyDictionary<string, string> CreateIcons() => new Dictionary<string, string>
    {
        ["+"] = "icon-plus-mobile",
        ["-"] = "icon-minus-mobile",
        ["="] = "icon-equals-mobile"
    };

    public PlatformLayout CreateLayout() => new("stack-mobile", "stack", ["7", "8", "9", "+", "=", "√"]);
}

public sealed class DesktopUiFactory : IPlatformUiFactory
{
    public string Platform => "desktop";

    public IReadOnlyDictionary<string, string> CreateIcons() => new Dictionary<string, string>
    {
        ["+"] = "icon-plus-desktop",
        ["-"] = "icon-minus-desktop",
        ["="] = "icon-equals-desktop"
    };

    public PlatformLayout CreateLayout() => new("dock-desktop", "dock", ["7", "8", "9", "+", "=", "√"]);
}

public sealed class PlatformUiFactoryProvider
{
    public IPlatformUiFactory GetFactory(string platform) => platform.Trim().ToLowerInvariant() switch
    {
        "web" => new WebUiFactory(),
        "mobile" => new MobileUiFactory(),
        "desktop" => new DesktopUiFactory(),
        _ => throw new NotSupportedException($"Platform '{platform}' is not supported.")
    };
}
