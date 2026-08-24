using Calculator.Api.Domain.Expressions;

namespace Calculator.Api.Domain.Builders;

/// <summary>
/// Defines the builder used to create a calculator expression tree.
/// </summary>
public interface IExpressionTreeBuilder
{
    /// <summary>
    /// Sets the expression to build.
    /// </summary>
    /// <param name="expression">The calculator expression.</param>
    /// <returns>The configured builder instance.</returns>
    IExpressionTreeBuilder WithExpression(string expression);

    /// <summary>
    /// Builds the expression tree.
    /// </summary>
    /// <returns>The root expression node.</returns>
    ExpressionNode Build();
}