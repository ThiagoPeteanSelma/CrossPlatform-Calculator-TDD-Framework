using Calculator.Shared.Contracts;

namespace Calculator.Api.Services.Interfaces;

/// <summary>
/// Defines calculation behavior for the API layer.
/// </summary>
public interface ICalculationService
{
    /// <summary>
    /// Calculates a result from the supplied request.
    /// </summary>
    /// <param name="request">The calculation request.</param>
    /// <returns>The calculation response.</returns>
    CalculationResponse Calculate(CalculationRequest request);
}