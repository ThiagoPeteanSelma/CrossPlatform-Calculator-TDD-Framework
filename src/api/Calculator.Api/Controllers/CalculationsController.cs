using Calculator.Api.Services.Interfaces;
using Calculator.Shared.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Api.Controllers;

/// <summary>
/// Exposes calculator operations through HTTP endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CanCalculate")]
public sealed class CalculationsController : ControllerBase
{
    private readonly ICalculationService calculationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculationsController"/> class.
    /// </summary>
    /// <param name="calculationService">The calculation service.</param>
    public CalculationsController(ICalculationService calculationService)
    {
        this.calculationService = calculationService;
    }

    /// <summary>
    /// Executes a calculation request.
    /// </summary>
    /// <param name="request">The calculation request payload.</param>
    /// <returns>The calculation result.</returns>
    [HttpPost]
    public ActionResult<CalculationResponse> Post([FromBody] CalculationRequest request)
    {
        if (request is null)
        {
            return BadRequest(new CalculationResponse
            {
                Success = false,
                ErrorMessage = "The request payload is required."
            });
        }

        bool hasExpression = !string.IsNullOrWhiteSpace(request.Expression);

        if (!hasExpression && request.LeftOperand is null)
        {
            return BadRequest(new CalculationResponse
            {
                Success = false,
                ErrorMessage = "Provide an expression or at least a left operand."
            });
        }

        CalculationResponse response = calculationService.Calculate(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}