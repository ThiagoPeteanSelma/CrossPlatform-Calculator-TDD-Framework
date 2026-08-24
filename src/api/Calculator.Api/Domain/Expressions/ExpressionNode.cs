namespace Calculator.Api.Domain.Expressions;

/// <summary>
/// Represents a node in a calculator expression tree.
/// </summary>
public abstract class ExpressionNode
{
    /// <summary>
    /// Evaluates the expression node.
    /// </summary>
    /// <returns>The calculated value.</returns>
    public abstract decimal Evaluate();
}