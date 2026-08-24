using Calculator.Api.Domain.Builders;
using Calculator.Api.Domain.Factories;
using Calculator.Api.Infrastructure.Configuration;
using Calculator.Api.Services;
using Calculator.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IOperationFactory, OperationFactory>();
builder.Services.AddSingleton<IExpressionBuilder, ExpressionBuilder>();
builder.Services.AddSingleton(_ => AppConfigurationManager.Instance);
builder.Services.AddScoped<ICalculationService, CalculationService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
