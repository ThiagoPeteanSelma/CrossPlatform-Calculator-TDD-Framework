using System.Globalization;

namespace CrossPlatformCalculator.Core.Models;

public sealed record OperationDefinition(string Symbol, string Name, bool RequiresSecondOperand);

public sealed record CalculationStep(string Symbol, decimal? Value = null);

public sealed record PlatformLayout(string LayoutId, string LayoutType, IReadOnlyList<string> Buttons);

public sealed class MathematicalExpression
{
    public MathematicalExpression(decimal initialValue, IReadOnlyList<CalculationStep> steps)
    {
        InitialValue = initialValue;
        Steps = steps;
        DisplayText = BuildDisplayText(initialValue, steps);
    }

    public decimal InitialValue { get; }

    public IReadOnlyList<CalculationStep> Steps { get; }

    public string DisplayText { get; }

    private static string BuildDisplayText(decimal initialValue, IReadOnlyList<CalculationStep> steps)
    {
        var expression = initialValue.ToString(CultureInfo.InvariantCulture);

        foreach (var step in steps)
        {
            expression = step.Symbol switch
            {
                "√" => $"√({expression})",
                "x²" => $"{expression} x²",
                "1/x" => $"1/({expression})",
                _ when step.Value is not null => $"{expression} {step.Symbol} {step.Value.Value.ToString(CultureInfo.InvariantCulture)}",
                _ => $"{step.Symbol}({expression})"
            };
        }

        return expression;
    }
}
