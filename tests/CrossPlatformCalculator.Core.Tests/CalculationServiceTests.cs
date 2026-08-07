using CrossPlatformCalculator.Core.Factories;
using CrossPlatformCalculator.Core.Services;

namespace CrossPlatformCalculator.Core.Tests;

public class CalculationServiceTests
{
    private readonly CalculationService _service = new(new DefaultOperationFactory());

    [Theory]
    [InlineData(4, "+", 5, 9)]
    [InlineData(9, "-", 4, 5)]
    [InlineData(3, "×", 4, 12)]
    [InlineData(8, "÷", 2, 4)]
    [InlineData(10, "%", 3, 1)]
    public void Calculate_executes_binary_operations(decimal left, string symbol, decimal right, decimal expected)
    {
        var result = _service.Calculate(left, symbol, right);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(16, "√", 4)]
    [InlineData(5, "x²", 25)]
    [InlineData(4, "1/x", 0.25)]
    public void Calculate_executes_unary_operations(decimal value, string symbol, decimal expected)
    {
        var result = _service.Calculate(value, symbol);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Calculate_throws_when_dividing_by_zero()
    {
        Assert.Throws<DivideByZeroException>(() => _service.Calculate(5, "÷", 0));
    }
}
