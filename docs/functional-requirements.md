# Functional Requirements

## Core Requirements
1. The system must support basic arithmetic operations: addition, subtraction, multiplication, and division.
2. The system must support advanced operations: modulus, square root, square, and reciprocal.
3. The system must allow the user to enter numeric values and select operations.
4. The system must accept expressions containing at least two numeric values and one operator.
5. The system must support formulas with no hard limit on the number of terms, as long as the expression remains valid and follows standard mathematical precedence.
6. The system must apply basic mathematical rules, including operator precedence and percentage handling, for expressions such as 1 + 2 - 6 / 4 * 8 + 10% = -10,2.
7. The system must return a calculated result to the frontend client after the equals action.
8. The API must centralize calculation logic and expose a consistent contract for all clients.

## Client Requirements
- Web client must provide a calculator UI using React.
- Mobile client must provide a calculator experience using .NET MAUI.
- Desktop client must provide a calculator experience using WPF.
- All clients should share the same calculation flow and behavior.

## Integration Requirements
- Frontend applications must communicate with the API using the shared contract.
- Errors from the API must be surfaced clearly to the user.
- The system must support both valid and invalid input scenarios.

## Testing Requirements
- Core calculations must be covered by automated unit tests.
- Integration scenarios must verify client-to-API communication.
- UI flows must be tested on the main supported clients.
