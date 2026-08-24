using System.Globalization;
using Calculator.Api.Domain.Factories;
using Calculator.Api.Services.Interfaces;
using Calculator.Api.Domain.Builders;
using Calculator.Shared.Contracts;

namespace Calculator.Api.Services;

/// <summary>
/// Provides basic calculation operations for the API.
/// </summary>
public sealed class CalculationService : ICalculationService
{
    private readonly IOperationFactory operationFactory;
    private readonly IExpressionTreeBuilder expressionTreeBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculationService"/> class.
    /// </summary>
    /// <param name="operationFactory">The operation factory.</param>
    /// <param name="expressionTreeBuilder">The expression tree builder.</param>
    public CalculationService(IOperationFactory operationFactory, IExpressionTreeBuilder expressionTreeBuilder)
    {
        this.operationFactory = operationFactory;
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

        try
        {
            result = operationFactory.Create(request.Operation).Execute(request);
        }
        catch (ArgumentException exception)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = exception.Message
            };
        }
        catch (DivideByZeroException exception)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = exception.Message
            };
        }
        catch (InvalidOperationException exception)
        {
            return new CalculationResponse
            {
                Success = false,
                ErrorMessage = exception.Message
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