# CrossPlatform Calculator TDD Framework

A compact reference implementation that demonstrates a unified calculator workflow for Web, Mobile, and Desktop clients with a shared .NET backend and focused TDD coverage.

## Objective

Provide one calculation structure for different environments while applying:
- SOLID principles in the domain and API layers
- Singleton, Factory Method, Abstract Factory, and Builder patterns
- TDD with focused unit tests

## Solution Structure

- `/src/CrossPlatformCalculator.Core` — shared calculation domain, design patterns, and application services
- `/src/CrossPlatformCalculator.Api` — REST API exposing calculation functions, platform icons, and layouts
- `/tests/CrossPlatformCalculator.Core.Tests` — targeted tests for the shared behavior
- `/CrossPlatformCalculator.slnx` — solution entry point

## Unified Flow

1. A client sends values and the selected operation to the API.
2. `CalculationService` delegates operation creation to `DefaultOperationFactory`.
3. The selected operation executes in the shared core.
4. The API returns the result to Web, Mobile, or Desktop clients.
5. UI clients can also request platform-specific icons and layouts from the API.

## Design Patterns Demonstrated

### Singleton
`AppConfiguration` centralizes global application settings and supported platforms/operations.

### Factory Method
`OperationFactory` and `DefaultOperationFactory` create the correct mathematical operation for symbols such as `+`, `÷`, `√`, and `1/x`.

### Abstract Factory
`IPlatformUiFactory` plus `WebUiFactory`, `MobileUiFactory`, and `DesktopUiFactory` provide icons and layouts per platform.

### Builder
`MathematicalExpressionBuilder` constructs multi-step expressions before evaluation by the shared service.

## SOLID Notes

- **S**ingle Responsibility: operations, factories, builders, and services each have one clear concern.
- **O**pen/Closed: new operations or platform factories can be added without changing consumers.
- **L**iskov Substitution: all operation implementations satisfy the same execution contract.
- **I**nterface Segregation: consumers depend on focused interfaces such as `ICalculationOperation` and `IPlatformUiFactory`.
- **D**ependency Inversion: `CalculationService` depends on the `OperationFactory` abstraction.

## API Endpoints

- `GET /api/config` — returns centralized configuration metadata
- `GET /api/functions` — lists available calculator functions
- `POST /api/calculations` — evaluates a single operation
- `POST /api/expressions` — evaluates a multi-step expression built from the shared builder
- `GET /api/platforms/{platform}/ui` — returns icons and layout metadata for `web`, `mobile`, or `desktop`

### Example Request

```json
POST /api/calculations
{
  "firstValue": 4,
  "operation": "+",
  "secondValue": 5
}
```

### Example Response

```json
{
  "expression": "4 + 5",
  "result": 9
}
```

## Architecture Note

The current structure follows an API-first layered approach that maps well to MVC-style separation of concerns. For larger growth, a Clean Architecture or Hexagonal Architecture evolution would improve scalability by isolating domain logic even further from transport and UI concerns.

## Run

```bash
dotnet run --project src/CrossPlatformCalculator.Api/CrossPlatformCalculator.Api.csproj
```

## Test

```bash
dotnet test tests/CrossPlatformCalculator.Core.Tests/CrossPlatformCalculator.Core.Tests.csproj
```
