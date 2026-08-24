using Calculator.Api.Domain.Operations;
using Calculator.Shared.Contracts;

namespace Calculator.Api.Domain.Factories;

/// <summary>
/// Factory method implementation for calculator operations.
/// </summary>
public sealed class OperationFactory : IOperationFactory
{
    /// <inheritdoc />
    public Calculator.Api.Domain.Operations.ICalculationOperation Create(CalculationOperation operation)
    {
        return operation switch
        {
            CalculationOperation.Add => new AddOperation(),
            CalculationOperation.Subtract => new SubtractOperation(),
            CalculationOperation.Multiply => new MultiplyOperation(),
            CalculationOperation.Divide => new DivideOperation(),
            CalculationOperation.Percentage => new PercentageOperation(),
            CalculationOperation.SquareRoot => new SquareRootOperation(),
            CalculationOperation.Square => new SquareOperation(),
            CalculationOperation.Reciprocal => new ReciprocalOperation(),
            _ => throw new InvalidOperationException($"The operation '{operation}' is not supported yet.")
        };
    }
}