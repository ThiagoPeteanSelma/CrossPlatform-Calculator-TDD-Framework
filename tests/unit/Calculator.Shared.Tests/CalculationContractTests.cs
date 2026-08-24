using Calculator.Shared.Contracts;
using Xunit;

namespace Calculator.Shared.Tests;

public sealed class CalculationContractTests
{
    [Fact]
    public void CalculationRequest_DefaultsToAddOperation()
    {
        CalculationRequest request = new();

        Assert.Equal(CalculationOperation.Add, request.Operation);
    }

    [Fact]
    public void CalculationResponse_DefaultsToUnsuccessfulState()
    {
        CalculationResponse response = new();

        Assert.False(response.Success);
        Assert.Null(response.Result);
        Assert.Null(response.FormattedResult);
        Assert.Null(response.ErrorMessage);
    }

    [Theory]
    [InlineData(CalculationOperation.Add)]
    [InlineData(CalculationOperation.Subtract)]
    [InlineData(CalculationOperation.Multiply)]
    [InlineData(CalculationOperation.Divide)]
    [InlineData(CalculationOperation.Percentage)]
    [InlineData(CalculationOperation.SquareRoot)]
    [InlineData(CalculationOperation.Square)]
    [InlineData(CalculationOperation.Reciprocal)]
    public void CalculationOperation_ContainsSupportedValues(CalculationOperation operation)
    {
        Assert.True(Enum.IsDefined(operation));
    }
}