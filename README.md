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
Example of use:
- A .NET Web API exposes calculation services.
- A React application consumes those services in the browser.
- A .NET MAUI app provides the same experience on mobile.
- A WPF app provides the same experience on desktop.
- The solution demonstrates SOLID principles, design patterns, and TDD across all platforms.

## Overview
CrossPlatform Calculator TDD Framework is a monorepo that demonstrates clean architecture, test-driven development, and cross-platform engineering practices. The solution is designed to unify calculation behavior across web, mobile, and desktop clients while keeping business logic centralized in the API.

## Architecture
- Backend: .NET Web API
- Frontend Web: React + Node.js
- Mobile: .NET MAUI
- Desktop: WPF
- Shared: common contracts, models, abstractions, and reusable services

## Project Structure
- docs/: architecture, API, deployment, and testing documentation
- src/: application projects for API, web, mobile, desktop, and shared logic
- tests/: unit, integration, and end-to-end tests
- scripts/: build and automation helpers
- .github/workflows/: CI/CD configuration

## Setup
1. Clone the repository.
2. Install .NET SDK, Node.js, and the required SDKs for MAUI and WPF development.
3. Restore dependencies for the backend and frontend projects.
4. Run the application using the appropriate project entry points.

## Testing
- API unit tests: xUnit
- React end-to-end tests: Playwright
- MAUI tests: MAUI test project
- WPF tests: NUnit

## Security
- Use HTTPS in production.
- Use JWT Bearer authentication for API access.
- Validate input and apply secure storage for mobile and desktop clients.
- Keep secrets and environment configuration out of source control.

## Contributing
Contributions are welcome. Please follow coding standards, add or update tests, and open a pull request with a clear description of the change.

## Documentation and Languages
The README and related documentation should be maintained in PT, EN, or ES, depending on the target audience.

## Notes
The BaseFiles folder is intentionally ignored by Git and should not contain project implementation files.

