namespace Calculator.Shared.Contracts;

/// <summary>
/// Defines the supported calculation operations exposed by the shared contract.
/// </summary>
public enum CalculationOperation
{
    /// <summary>
    /// Adds two values.
    /// </summary>
    Add,

    /// <summary>
    /// Subtracts the second value from the first.
    /// </summary>
    Subtract,

    /// <summary>
    /// Multiplies two values.
    /// </summary>
    Multiply,

    /// <summary>
    /// Divides the first value by the second.
    /// </summary>
    Divide,

    /// <summary>
    /// Calculates a percentage value.
    /// </summary>
    Percentage,

    /// <summary>
    /// Calculates the square root of the first value.
    /// </summary>
    SquareRoot,

    /// <summary>
    /// Calculates the square of the first value.
    /// </summary>
    Square,

    /// <summary>
    /// Calculates the reciprocal of the first value.
    /// </summary>
    Reciprocal
}