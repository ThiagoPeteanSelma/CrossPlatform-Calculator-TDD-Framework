using Calculator.Api.Domain.Operations;
using Calculator.Shared.Contracts;

namespace Calculator.Api.Domain.Factories;

/// <summary>
/// Creates calculator operations from the shared contract enum.
/// </summary>
public interface IOperationFactory
{
    /// <summary>
    /// Creates the operation implementation for the requested calculator operation.
    /// </summary>
    /// <param name="operation">The requested calculation operation.</param>
    /// <returns>The operation implementation.</returns>
    Calculator.Api.Domain.Operations.ICalculationOperation Create(CalculationOperation operation);
}