using Microsoft.Extensions.Configuration;

namespace Calculator.Api.Infrastructure.Configuration;

/// <summary>
/// Centralizes application configuration access for the API.
/// </summary>
public sealed class AppConfigurationManager
{
    private static readonly Lazy<AppConfigurationManager> LazyInstance = new(() => new AppConfigurationManager());

    private AppConfigurationManager()
    {
    }

    /// <summary>
    /// Gets the singleton configuration manager instance.
    /// </summary>
    public static AppConfigurationManager Instance => LazyInstance.Value;

    /// <summary>
    /// Gets the JWT settings loaded for the application.
    /// </summary>
    public JwtOptions JwtOptions { get; private set; } = new();

    /// <summary>
    /// Loads strongly typed configuration values into the singleton.
    /// </summary>
    /// <param name="configuration">The application configuration source.</param>
    public void Initialize(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        JwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
    }
}