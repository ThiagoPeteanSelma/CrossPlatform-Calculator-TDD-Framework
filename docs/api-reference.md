# API Reference

## Overview

The API is the central calculator service for the monorepo and currently exposes a single calculation endpoint.

## Base URL

- Development: `https://localhost:5001`
- HTTP redirection is enabled in development and production should use HTTPS only.

## Authentication

- The endpoint is protected by JWT Bearer authentication and the `CanCalculate` policy.

## Endpoints

### POST /api/calculations

Executes either a direct operation or an expression-based calculation.

Request body:

```json
{
  "leftOperand": 7,
  "rightOperand": 5,
  "operation": "Subtract"
}
```

Expression request:

```json
{
  "expression": "1 + 2 * 3 - 4 / 2"
}
```

Response:

```json
{
  "success": true,
  "result": 2,
  "formattedResult": "2",
  "errorMessage": null
}
```

## Error Handling

- `400 Bad Request` for invalid payloads or unsupported operations.
- `401 Unauthorized` for missing or invalid credentials.
- `403 Forbidden` for requests that do not satisfy the authorization policy.

## Notes

- Expressions honor operator precedence and percentage suffix handling.
- Direct operations are implemented in the API layer through an operation factory.
