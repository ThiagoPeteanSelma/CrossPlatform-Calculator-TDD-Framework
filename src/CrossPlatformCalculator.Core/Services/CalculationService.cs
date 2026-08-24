using CrossPlatformCalculator.Core.Factories;
using CrossPlatformCalculator.Core.Models;

namespace CrossPlatformCalculator.Core.Services;

public sealed class CalculationService
{
    private readonly OperationFactory _operationFactory;

    public CalculationService(OperationFactory operationFactory)
    {
        _operationFactory = operationFactory;
    }

    public IReadOnlyCollection<OperationDefinition> GetAvailableOperations() => _operationFactory.GetAvailableOperations();

    public decimal Calculate(decimal firstValue, string symbol, decimal? secondValue = null)
    {
        var operation = _operationFactory.CreateOperation(symbol);
        return operation.Execute(firstValue, secondValue);
    }

    public decimal Evaluate(MathematicalExpression expression)
    {
        var currentValue = expression.InitialValue;

        foreach (var step in expression.Steps)
        {
            currentValue = Calculate(currentValue, step.Symbol, step.Value);
        }

        return currentValue;
    }
}
