namespace Calculator.Shared.Contracts;

/// <summary>
/// Represents a calculation request shared between the API and client applications.
/// </summary>
public sealed record CalculationRequest
{
    /// <summary>
    /// Gets or sets the left-hand operand.
    /// </summary>
    public decimal? LeftOperand { get; init; }

    /// <summary>
    /// Gets or sets the right-hand operand.
    /// </summary>
    public decimal? RightOperand { get; init; }

    /// <summary>
    /// Gets or sets the operation to execute.
    /// </summary>
    public CalculationOperation Operation { get; init; } = CalculationOperation.Add;

    /// <summary>
    /// Gets or sets an optional expression for future formula-based calculations.
    /// </summary>
    public string? Expression { get; init; }
}