using System.Globalization;
using Calculator.Api.Domain.Expressions;

namespace Calculator.Api.Domain.Builders;

/// <summary>
/// Builds and parses calculator expressions with operator precedence.
/// </summary>
public sealed class ExpressionTreeBuilder : IExpressionTreeBuilder
{
    private string expression = string.Empty;
    private int position;

    /// <inheritdoc />
    public IExpressionTreeBuilder WithExpression(string expression)
    {
        this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
        position = 0;
        return this;
    }

    /// <inheritdoc />
    public ExpressionNode Build()
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new FormatException("An expression is required.");
        }

        position = 0;
        ExpressionNode root = ParseExpression();
        SkipWhitespace();

        if (!IsEnd)
        {
            throw new FormatException($"Unexpected token at position {position}.");
        }

        return root;
    }

    private ExpressionNode ParseExpression()
    {
        ExpressionNode node = ParseTerm();

        while (true)
        {
            SkipWhitespace();

            if (Match('+'))
            {
                node = new BinaryExpressionNode(node, ParseTerm(), BinaryOperator.Add);
                continue;
            }

            if (Match('-'))
            {
                node = new BinaryExpressionNode(node, ParseTerm(), BinaryOperator.Subtract);
                continue;
            }

            break;
        }

        return node;
    }

    private ExpressionNode ParseTerm()
    {
        ExpressionNode node = ParseFactor();

        while (true)
        {
            SkipWhitespace();

            if (Match('*'))
            {
                node = new BinaryExpressionNode(node, ParseFactor(), BinaryOperator.Multiply);
                continue;
            }

            if (Match('/'))
            {
                node = new BinaryExpressionNode(node, ParseFactor(), BinaryOperator.Divide);
                continue;
            }

            break;
        }

        return node;
    }

    private ExpressionNode ParseFactor()
    {
        SkipWhitespace();

        if (Match('+'))
        {
            return ParseFactor();
        }

        if (Match('-'))
        {
            return new UnaryExpressionNode(ParseFactor(), UnaryOperator.Negate);
        }

        ExpressionNode node = ParsePrimary();

        while (true)
        {
            SkipWhitespace();

            if (!Match('%'))
            {
                break;
            }

            node = new UnaryExpressionNode(node, UnaryOperator.Percentage);
        }

        return node;
    }

    private ExpressionNode ParsePrimary()
    {
        SkipWhitespace();

        if (Match('('))
        {
            ExpressionNode node = ParseExpression();
            SkipWhitespace();

            if (!Match(')'))
            {
                throw new FormatException($"Missing closing parenthesis at position {position}.");
            }

            return node;
        }

        return ParseNumber();
    }

    private ExpressionNode ParseNumber()
    {
        SkipWhitespace();

        int start = position;

        while (!IsEnd && (char.IsDigit(Current) || Current is '.' or ','))
        {
            position++;
        }

        if (start == position)
        {
            throw new FormatException($"Number expected at position {position}.");
        }

        string rawNumber = expression[start..position].Replace(',', '.');

        if (!decimal.TryParse(rawNumber, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value))
        {
            throw new FormatException($"Invalid number '{rawNumber}'.");
        }

        return new NumberExpressionNode(value);
    }

    private void SkipWhitespace()
    {
        while (!IsEnd && char.IsWhiteSpace(Current))
        {
            position++;
        }
    }

    private bool Match(char expected)
    {
        if (IsEnd || Current != expected)
        {
            return false;
        }

        position++;
        return true;
    }

    private bool IsEnd => position >= expression.Length;

    private char Current => expression[position];

    private sealed class NumberExpressionNode : ExpressionNode
    {
        private readonly decimal value;

        public NumberExpressionNode(decimal value)
        {
            this.value = value;
        }

        public override decimal Evaluate() => value;
    }

    private sealed class UnaryExpressionNode : ExpressionNode
    {
        private readonly ExpressionNode operand;
        private readonly UnaryOperator unaryOperator;

        public UnaryExpressionNode(ExpressionNode operand, UnaryOperator unaryOperator)
        {
            this.operand = operand;
            this.unaryOperator = unaryOperator;
        }

        public override decimal Evaluate()
        {
            decimal value = operand.Evaluate();

            return unaryOperator switch
            {
                UnaryOperator.Negate => -value,
                UnaryOperator.Percentage => value / 100m,
                _ => throw new InvalidOperationException("Unsupported unary operator.")
            };
        }
    }

    private sealed class BinaryExpressionNode : ExpressionNode
    {
        private readonly ExpressionNode left;
        private readonly ExpressionNode right;
        private readonly BinaryOperator binaryOperator;

        public BinaryExpressionNode(ExpressionNode left, ExpressionNode right, BinaryOperator binaryOperator)
        {
            this.left = left;
            this.right = right;
            this.binaryOperator = binaryOperator;
        }

        public override decimal Evaluate()
        {
            decimal leftValue = left.Evaluate();
            decimal rightValue = right.Evaluate();

            return binaryOperator switch
            {
                BinaryOperator.Add => leftValue + rightValue,
                BinaryOperator.Subtract => leftValue - rightValue,
                BinaryOperator.Multiply => leftValue * rightValue,
                BinaryOperator.Divide => rightValue == 0m
                    ? throw new DivideByZeroException("Division by zero is not allowed.")
                    : leftValue / rightValue,
                _ => throw new InvalidOperationException("Unsupported binary operator.")
            };
        }
    }

    private enum BinaryOperator
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    private enum UnaryOperator
    {
        Negate,
        Percentage
    }
}