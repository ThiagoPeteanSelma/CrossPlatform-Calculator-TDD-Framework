using CrossPlatformCalculator.Core.Builders;
using CrossPlatformCalculator.Core.Configuration;
using CrossPlatformCalculator.Core.Factories;
using CrossPlatformCalculator.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(AppConfiguration.Instance);
builder.Services.AddSingleton<OperationFactory, DefaultOperationFactory>();
builder.Services.AddSingleton<PlatformUiFactoryProvider>();
builder.Services.AddScoped<CalculationService>();

var app = builder.Build();

app.MapGet("/api/config", (AppConfiguration configuration) => Results.Ok(new
{
    configuration.ApplicationName,
    configuration.Architecture,
    configuration.SupportedPlatforms,
    configuration.SupportedOperations
}));

app.MapGet("/api/functions", (CalculationService service) => Results.Ok(service.GetAvailableOperations()));

app.MapPost("/api/calculations", (CalculationRequest request, CalculationService service) =>
    Execute(() =>
    {
        var result = service.Calculate(request.FirstValue, request.Operation, request.SecondValue);
        var expressionBuilder = BuildExpression(request.FirstValue, [new ExpressionStepRequest(request.Operation, request.SecondValue)]);
        var expression = expressionBuilder.Build();

        return Results.Ok(new CalculationResponse(expression.DisplayText, result));
    }));

app.MapPost("/api/expressions", (ExpressionRequest request, CalculationService service) =>
    Execute(() =>
    {
        var expressionBuilder = BuildExpression(request.InitialValue, request.Steps);
        var expression = expressionBuilder.Build();
        var result = service.Evaluate(expression);

        return Results.Ok(new CalculationResponse(expression.DisplayText, result));
    }));

app.MapGet("/api/platforms/{platform}/ui", (string platform, PlatformUiFactoryProvider provider) =>
    Execute(() =>
    {
        var factory = provider.GetFactory(platform);
        return Results.Ok(new
        {
            factory.Platform,
            Icons = factory.CreateIcons(),
            Layout = factory.CreateLayout()
        });
    }));

app.Run();

static IResult Execute(Func<IResult> action)
{
    try
    {
        return action();
    }
    catch (Exception exception) when (exception is NotSupportedException or InvalidOperationException or DivideByZeroException)
    {
        return Results.BadRequest(new { error = exception.Message });
    }
}

static MathematicalExpressionBuilder BuildExpression(decimal initialValue, IReadOnlyList<ExpressionStepRequest> steps)
{
    var builder = new MathematicalExpressionBuilder(initialValue);

    foreach (var step in steps)
    {
        ApplyStep(builder, step);
    }

    return builder;
}

static void ApplyStep(MathematicalExpressionBuilder builder, ExpressionStepRequest step)
{
    var operation = step.Operation.Trim();

    switch (operation.ToLowerInvariant())
    {
        case "+":
            builder.Add(RequireValue(step));
            break;
        case "-":
            builder.Subtract(RequireValue(step));
            break;
        case "×":
        case "*":
            builder.Multiply(RequireValue(step));
            break;
        case "÷":
        case "/":
            builder.Divide(RequireValue(step));
            break;
        case "%":
            builder.Modulo(RequireValue(step));
            break;
        case "√":
        case "sqrt":
            builder.SquareRoot();
            break;
        case "x²":
        case "square":
            builder.Square();
            break;
        case "1/x":
        case "reciprocal":
            builder.Reciprocal();
            break;
        default:
            throw new NotSupportedException($"Operation '{step.Operation}' is not supported.");
    }
}

static decimal RequireValue(ExpressionStepRequest step) =>
    step.Value ?? throw new InvalidOperationException($"Operation '{step.Operation}' requires a second operand.");

public sealed record CalculationRequest(decimal FirstValue, string Operation, decimal? SecondValue = null);
public sealed record ExpressionStepRequest(string Operation, decimal? Value = null);
public sealed record ExpressionRequest(decimal InitialValue, IReadOnlyList<ExpressionStepRequest> Steps);
public sealed record CalculationResponse(string Expression, decimal Result);
