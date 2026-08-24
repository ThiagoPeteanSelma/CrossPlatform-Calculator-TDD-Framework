using System.Globalization;
using Calculator.Api.Services.Interfaces;
using Calculator.Api.Domain.Builders;
using Calculator.Shared.Contracts;

namespace Calculator.Api.Services;

/// <summary>
/// Provides basic calculation operations for the API.
/// </summary>
public sealed class CalculationService : ICalculationService
{
    private readonly IExpressionTreeBuilder expressionTreeBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculationService"/> class.
    /// </summary>
    /// <param name="expressionTreeBuilder">The expression tree builder.</param>
    public CalculationService(IExpressionTreeBuilder expressionTreeBuilder)
    {
        this.expressionTreeBuilder = expressionTreeBuilder;
    }

    /// <inheritdoc />
    public CalculationResponse Calculate(CalculationRequest request)
    {
        if (request is null)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = "The request payload is required."
            };
        }

        if (!string.IsNullOrWhiteSpace(request.Expression))
        {
            return EvaluateExpression(request.Expression);
        }

        if (request.LeftOperand is null)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = "The left operand is required."
            };
        }

        decimal result;

        switch (request.Operation)
        {
            case CalculationOperation.Add:
                result = request.LeftOperand.Value + (request.RightOperand ?? 0m);
                break;
            case CalculationOperation.Subtract:
                result = request.LeftOperand.Value - (request.RightOperand ?? 0m);
                break;
            case CalculationOperation.Multiply:
                result = request.LeftOperand.Value * (request.RightOperand ?? 1m);
                break;
            case CalculationOperation.Divide:
                if (request.RightOperand is null || request.RightOperand.Value == 0m)
                {
                    return new CalculationResponse
                    {
                        Success = false,
                        ErrorMessage = "The right operand must be different from zero for division."
                    };
                }

                result = request.LeftOperand.Value / request.RightOperand.Value;
                break;
            case CalculationOperation.Percentage:
                result = request.LeftOperand.Value / 100m;
                break;
            case CalculationOperation.SquareRoot:
                if (request.LeftOperand.Value < 0m)
                {
                    return new CalculationResponse
                    {
                        Success = false,
                        ErrorMessage = "The square root is not defined for negative values."
                    };
                }

                result = (decimal)Math.Sqrt((double)request.LeftOperand.Value);
                break;
            case CalculationOperation.Square:
                result = request.LeftOperand.Value * request.LeftOperand.Value;
                break;
            case CalculationOperation.Reciprocal:
                if (request.LeftOperand.Value == 0m)
                {
                    return new CalculationResponse
                    {
                        Success = false,
                        ErrorMessage = "The reciprocal is not defined for zero."
                    };
                }

                result = 1m / request.LeftOperand.Value;
                break;
            default:
                return new CalculationResponse
                {
                    Success = false,
                    ErrorMessage = $"The operation '{request.Operation}' is not supported yet."
                };
        }

        return new CalculationResponse
        {
            Success = true,
            Result = result,
            FormattedResult = result.ToString("G29", CultureInfo.InvariantCulture)
        };
    }

    private CalculationResponse EvaluateExpression(string expression)
    {
        try
        {
            decimal result = expressionTreeBuilder
                .WithExpression(expression)
                .Build()
                .Evaluate();

            return new CalculationResponse
            {
                Success = true,
                Result = result,
                FormattedResult = result.ToString("G29", CultureInfo.InvariantCulture)
            };
        }
        catch (Exception exception) when (exception is FormatException or DivideByZeroException or InvalidOperationException)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = exception.Message
            };
        }
    }
}