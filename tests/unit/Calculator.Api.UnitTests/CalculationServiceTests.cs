using Calculator.Api.Domain.Builders;
using Calculator.Api.Domain.Factories;
using Calculator.Api.Services;
using Calculator.Shared.Contracts;
using Xunit;

namespace Calculator.Api.UnitTests;

public sealed class CalculationServiceTests
{
    private readonly CalculationService service;

    public CalculationServiceTests()
    {
        service = new CalculationService(new OperationFactory(), new ExpressionTreeBuilder());
    }

    [Fact]
    public void Calculate_WhenExpressionHasOperatorPrecedence_ReturnsExpectedResult()
    {
        CalculationResponse response = service.Calculate(new CalculationRequest
        {
            Expression = "1 + 2 * 3 - 4 / 2"
        });

        Assert.True(response.Success);
        Assert.Equal(5m, response.Result);
        Assert.Equal("5", response.FormattedResult);
    }

    [Fact]
    public void Calculate_WhenExpressionHasParentheses_ReturnsExpectedResult()
    {
        CalculationResponse response = service.Calculate(new CalculationRequest
        {
            Expression = "(1 + 2) * 3"
        });

        Assert.True(response.Success);
        Assert.Equal(9m, response.Result);
        Assert.Equal("9", response.FormattedResult);
    }

    [Fact]
    public void Calculate_WhenExpressionHasPercentageSuffix_ReturnsFraction()
    {
        CalculationResponse response = service.Calculate(new CalculationRequest
        {
            Expression = "10%"
        });

        Assert.True(response.Success);
        Assert.Equal(0.1m, response.Result);
        Assert.Equal("0.1", response.FormattedResult);
    }

    [Fact]
    public void Calculate_WhenExpressionIsInvalid_ReturnsFailure()
    {
        CalculationResponse response = service.Calculate(new CalculationRequest
        {
            Expression = "1 + * 2"
        });

        Assert.False(response.Success);
        Assert.NotNull(response.ErrorMessage);
    }

    [Fact]
    public void Calculate_WhenNoExpressionProvided_UsesBasicOperation()
    {
        CalculationResponse response = service.Calculate(new CalculationRequest
        {
            LeftOperand = 7m,
            RightOperand = 5m,
            Operation = CalculationOperation.Subtract
        });

        Assert.True(response.Success);
        Assert.Equal(2m, response.Result);
    }
}