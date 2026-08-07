namespace CrossPlatformCalculator.Core.Operations;

public interface ICalculationOperation
{
    string Symbol { get; }

    string Name { get; }

    bool RequiresSecondOperand { get; }

    decimal Execute(decimal left, decimal? right = null);
}

public abstract class CalculationOperationBase : ICalculationOperation
{
    public abstract string Symbol { get; }

    public abstract string Name { get; }

    public abstract bool RequiresSecondOperand { get; }

    public decimal Execute(decimal left, decimal? right = null)
    {
        if (RequiresSecondOperand && right is null)
        {
            throw new InvalidOperationException($"Operation '{Symbol}' requires a second operand.");
        }

        return ExecuteCore(left, right);
    }

    protected abstract decimal ExecuteCore(decimal left, decimal? right);
}

public sealed class AdditionOperation : CalculationOperationBase
{
    public override string Symbol => "+";
    public override string Name => "Addition";
    public override bool RequiresSecondOperand => true;
    protected override decimal ExecuteCore(decimal left, decimal? right) => left + right!.Value;
}

public sealed class SubtractionOperation : CalculationOperationBase
{
    public override string Symbol => "-";
    public override string Name => "Subtraction";
    public override bool RequiresSecondOperand => true;
    protected override decimal ExecuteCore(decimal left, decimal? right) => left - right!.Value;
}

public sealed class MultiplicationOperation : CalculationOperationBase
{
    public override string Symbol => "×";
    public override string Name => "Multiplication";
    public override bool RequiresSecondOperand => true;
    protected override decimal ExecuteCore(decimal left, decimal? right) => left * right!.Value;
}

public sealed class DivisionOperation : CalculationOperationBase
{
    public override string Symbol => "÷";
    public override string Name => "Division";
    public override bool RequiresSecondOperand => true;

    protected override decimal ExecuteCore(decimal left, decimal? right)
    {
        if (right == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }

        return left / right!.Value;
    }
}

public sealed class ModuloOperation : CalculationOperationBase
{
    public override string Symbol => "%";
    public override string Name => "Modulo";
    public override bool RequiresSecondOperand => true;

    protected override decimal ExecuteCore(decimal left, decimal? right)
    {
        if (right == 0)
        {
            throw new DivideByZeroException("Modulo by zero is not allowed.");
        }

        return left % right!.Value;
    }
}

public sealed class SquareRootOperation : CalculationOperationBase
{
    public override string Symbol => "√";
    public override string Name => "Square Root";
    public override bool RequiresSecondOperand => false;

    protected override decimal ExecuteCore(decimal left, decimal? right)
    {
        if (left < 0)
        {
            throw new InvalidOperationException("Square root is not defined for negative values.");
        }

        return (decimal)Math.Sqrt((double)left);
    }
}

public sealed class SquareOperation : CalculationOperationBase
{
    public override string Symbol => "x²";
    public override string Name => "Square";
    public override bool RequiresSecondOperand => false;
    protected override decimal ExecuteCore(decimal left, decimal? right) => left * left;
}

public sealed class ReciprocalOperation : CalculationOperationBase
{
    public override string Symbol => "1/x";
    public override string Name => "Reciprocal";
    public override bool RequiresSecondOperand => false;

    protected override decimal ExecuteCore(decimal left, decimal? right)
    {
        if (left == 0)
        {
            throw new DivideByZeroException("Reciprocal is not defined for zero.");
        }

        return 1 / left;
    }
}
