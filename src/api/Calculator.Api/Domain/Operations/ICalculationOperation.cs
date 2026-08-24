using Calculator.Shared.Contracts;

namespace Calculator.Api.Domain.Operations;

/// <summary>
/// Represents a calculator operation implementation.
/// </summary>
public interface ICalculationOperation
{
    /// <summary>
    /// Executes the operation using the supplied request.
    /// </summary>
    /// <param name="request">The calculation request.</param>
    /// <returns>The calculated value.</returns>
    decimal Execute(CalculationRequest request);
}