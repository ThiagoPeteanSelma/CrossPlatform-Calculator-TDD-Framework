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
        var expression = request.SecondValue is null
            ? $"{request.Operation}({request.FirstValue})"
            : $"{request.FirstValue} {request.Operation} {request.SecondValue}";

        return Results.Ok(new CalculationResponse(expression, result));
    }));

app.MapPost("/api/expressions", (ExpressionRequest request, CalculationService service) =>
    Execute(() =>
    {
        var expressionBuilder = new MathematicalExpressionBuilder(request.InitialValue);

        foreach (var step in request.Steps)
        {
            expressionBuilder.Apply(step.Operation, step.Value);
        }

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

public sealed record CalculationRequest(decimal FirstValue, string Operation, decimal? SecondValue = null);
public sealed record ExpressionStepRequest(string Operation, decimal? Value = null);
public sealed record ExpressionRequest(decimal InitialValue, IReadOnlyList<ExpressionStepRequest> Steps);
public sealed record CalculationResponse(string Expression, decimal Result);
