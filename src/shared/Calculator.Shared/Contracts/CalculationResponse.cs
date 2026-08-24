namespace Calculator.Shared.Contracts;

/// <summary>
/// Represents the result of a calculation request.
/// </summary>
public sealed record CalculationResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the request was processed successfully.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Gets or sets the calculated result when the operation succeeds.
    /// </summary>
    public decimal? Result { get; init; }

    /// <summary>
    /// Gets or sets a formatted representation of the result.
    /// </summary>
    public string? FormattedResult { get; init; }

    /// <summary>
    /// Gets or sets the error message when the request fails.
    /// </summary>
    public string? ErrorMessage { get; init; }
}