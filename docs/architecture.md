# Architecture

This repository uses a layered monorepo structure with a central .NET Web API as the current business logic layer.

- `src/api/Calculator.Api` contains the API, calculation service, builders, factories, and configuration.
- `src/shared/Calculator.Shared` contains request and response contracts shared across clients.
- `tests/unit` contains unit tests for the API and shared contracts.
- `src/web`, `src/mobile`, and `src/desktop` are reserved for future client implementations.

The active solution is `Calculator.sln`.
