# API Reference

## Overview
The API provides calculation services for the calculator monorepo and acts as the central business logic layer for web, mobile, and desktop clients.

## Base URL
- Development: http://localhost:5000
- Production: https://api.example.com

## Authentication
- JWT Bearer authentication is recommended for protected endpoints.
- Public endpoints may be used for health checks and basic calculator operations.

## Endpoints

### GET /health
Checks the service availability.

Response:
```json
{
  "status": "ok"
}
```

### POST /api/calculator/compute
Executes a calculation based on a formula supplied by the client.

Request body:
```json
{
  "expression": "1 + 2 - 6 / 4 * 8 + 10%"
}
```

The API must accept expressions with at least two numeric values and one operator, and it should support formulas with no hard limit on the number of terms. The parser must apply standard mathematical precedence and percentage rules.

Supported operations:
- add
- subtract
- multiply
- divide
- modulus
- percentage
- squareRoot
- square
- reciprocal

Response:
```json
{
  "result": -10.2,
  "expression": "1 + 2 - 6 / 4 * 8 + 10%",
  "timestamp": "2026-08-07T00:00:00Z"
}
```

### POST /api/calculator/validate
Validates an incoming calculation request before execution.

## Error Handling
- 400 Bad Request for malformed data.
- 401 Unauthorized for missing or invalid credentials.
- 403 Forbidden for insufficient permissions.
- 500 Internal Server Error for unexpected failures.

## Notes
- All client input must be validated server-side.
- The API should return structured errors and logs for monitoring.
