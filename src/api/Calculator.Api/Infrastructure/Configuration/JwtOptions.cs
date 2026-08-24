namespace Calculator.Api.Infrastructure.Configuration;

/// <summary>
/// Represents JWT bearer settings used by the API.
/// </summary>
public sealed class JwtOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "Jwt";

    /// <summary>
    /// Gets or sets the token issuer.
    /// </summary>
    public string Issuer { get; init; } = "CrossPlatformCalculator";

    /// <summary>
    /// Gets or sets the token audience.
    /// </summary>
    public string Audience { get; init; } = "CrossPlatformCalculatorClients";

    /// <summary>
    /// Gets or sets the signing key fallback used in development.
    /// </summary>
    public string Key { get; init; } = "replace-this-development-key-with-env-var";
}
