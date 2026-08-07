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

    public MathematicalExpressionBuilder Add(decimal value) => Apply("+", value);

    public MathematicalExpressionBuilder Subtract(decimal value) => Apply("-", value);

    public MathematicalExpressionBuilder Multiply(decimal value) => Apply("×", value);

    public MathematicalExpressionBuilder Divide(decimal value) => Apply("÷", value);

    public MathematicalExpressionBuilder Modulo(decimal value) => Apply("%", value);

    public MathematicalExpressionBuilder SquareRoot() => Apply("√");

    public MathematicalExpressionBuilder Square() => Apply("x²");

    public MathematicalExpressionBuilder Reciprocal() => Apply("1/x");

    public MathematicalExpressionBuilder Apply(string symbol, decimal? value = null)
    {
        _steps.Add(new CalculationStep(symbol, value));
        return this;
    }

    public MathematicalExpression Build() => new(InitialValue, _steps.AsReadOnly());
}
