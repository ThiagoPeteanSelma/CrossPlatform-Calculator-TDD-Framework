using CrossPlatformCalculator.Core.Models;

namespace CrossPlatformCalculator.Core.Builders;

public sealed class MathematicalExpressionBuilder
{
    private readonly List<CalculationStep> _steps = [];

    public MathematicalExpressionBuilder(decimal initialValue)
    {
        InitialValue = initialValue;
    }

    public decimal InitialValue { get; }

    public MathematicalExpressionBuilder Add(decimal value) => AddStep("+", value);

    public MathematicalExpressionBuilder Subtract(decimal value) => AddStep("-", value);

    public MathematicalExpressionBuilder Multiply(decimal value) => AddStep("×", value);

    public MathematicalExpressionBuilder Divide(decimal value) => AddStep("÷", value);

    public MathematicalExpressionBuilder Modulo(decimal value) => AddStep("%", value);

    public MathematicalExpressionBuilder SquareRoot() => AddStep("√");

    public MathematicalExpressionBuilder Square() => AddStep("x²");

    public MathematicalExpressionBuilder Reciprocal() => AddStep("1/x");

    public MathematicalExpression Build() => new(InitialValue, _steps.AsReadOnly());

    private MathematicalExpressionBuilder AddStep(string symbol, decimal? value = null)
    {
        _steps.Add(new CalculationStep(symbol, value));
        return this;
    }
}
