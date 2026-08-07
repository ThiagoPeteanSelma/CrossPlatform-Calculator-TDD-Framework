# CrossPlatform Calculator TDD Framework

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

