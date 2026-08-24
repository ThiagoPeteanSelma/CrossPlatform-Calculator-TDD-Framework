# Architecture

This project follows a layered monorepo architecture with a central .NET Web API serving as the business logic layer.

- API: exposes calculation services.
- Web: React client consuming the API.
- Mobile: .NET MAUI client.
- Desktop: WPF client.
- Shared: common contracts, models, and abstractions.
