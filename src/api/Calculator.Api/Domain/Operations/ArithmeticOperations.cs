using Calculator.Shared.Contracts;

namespace Calculator.Api.Domain.Operations;

internal sealed class AddOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);
        return request.LeftOperand!.Value + (request.RightOperand ?? 0m);
    }
}

internal sealed class SubtractOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);
        return request.LeftOperand!.Value - (request.RightOperand ?? 0m);
    }
}

internal sealed class MultiplyOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);
        return request.LeftOperand!.Value * (request.RightOperand ?? 1m);
    }
}

internal sealed class DivideOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);

        if (request.RightOperand is null || request.RightOperand.Value == 0m)
        {
            throw new DivideByZeroException("The right operand must be different from zero for division.");
        }

        return request.LeftOperand!.Value / request.RightOperand.Value;
    }
}

internal sealed class PercentageOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);
        return request.LeftOperand!.Value / 100m;
    }
}

internal sealed class SquareRootOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);

        if (request.LeftOperand!.Value < 0m)
        {
            throw new InvalidOperationException("The square root is not defined for negative values.");
        }

        return (decimal)Math.Sqrt((double)request.LeftOperand.Value);
    }
}

internal sealed class SquareOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);
        decimal value = request.LeftOperand!.Value;
        return value * value;
    }
}

internal sealed class ReciprocalOperation : ICalculationOperation
{
    public decimal Execute(CalculationRequest request)
    {
        CalculationOperationGuard.EnsureLeftOperand(request);

        if (request.LeftOperand!.Value == 0m)
        {
            throw new InvalidOperationException("The reciprocal is not defined for zero.");
        }

        return 1m / request.LeftOperand.Value;
    }
}

internal static class CalculationOperationGuard
{
    public static void EnsureLeftOperand(CalculationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.LeftOperand is null)
        {
            throw new ArgumentException("The left operand is required.", nameof(request));
        }
    }
}