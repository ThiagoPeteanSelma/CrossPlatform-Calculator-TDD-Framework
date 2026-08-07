using CrossPlatformCalculator.Core.Builders;
using CrossPlatformCalculator.Core.Factories;
using CrossPlatformCalculator.Core.Services;

namespace CrossPlatformCalculator.Core.Tests;

public class MathematicalExpressionBuilderTests
{
    [Fact]
    public void Builder_creates_and_evaluates_complex_expression()
    {
        var builder = new MathematicalExpressionBuilder(9)
            .SquareRoot()
            .Add(2)
            .Square();

        var expression = builder.Build();
        var service = new CalculationService(new DefaultOperationFactory());

        var result = service.Evaluate(expression);

        Assert.Equal("√(9) + 2 x²", expression.DisplayText);
        Assert.Equal(25, result);
    }
}
