# CrossPlatform Calculator TDD Framework

Monorepo for a cross-platform calculator built around a central .NET Web API, shared contracts, and platform clients planned for web, mobile, and desktop.

## Current Structure

- `src/api/Calculator.Api` - central calculator API and business logic
- `src/shared/Calculator.Shared` - shared contracts used by the API and clients
- `tests/unit/Calculator.Api.UnitTests` - API unit tests
- `tests/unit/Calculator.Shared.Tests` - shared contract tests
- `src/web`, `src/mobile`, `src/desktop` - placeholders for future clients

## Architecture

- The API is the current source of truth for calculation behavior.
- Shared contracts keep requests and responses consistent across platforms.
- The current backend implementation demonstrates Builder, Factory Method, and Singleton usage.
- The solution file in use is `Calculator.sln`.

## Run

```bash
dotnet run --project src/api/Calculator.Api/Calculator.Api.csproj
```

## Test

```bash
dotnet test Calculator.sln
```

## Documentation

- `docs/architecture.md` describes the active layering.
- `docs/api-reference.md` documents the current API surface.
- `docs/deployment-guide.md` covers deployment options and security notes.

## Notes

- `BaseFiles` is for planning and must stay out of source control.
- Legacy `CrossPlatformCalculator.*` projects were removed to avoid duplicate solution roots.

