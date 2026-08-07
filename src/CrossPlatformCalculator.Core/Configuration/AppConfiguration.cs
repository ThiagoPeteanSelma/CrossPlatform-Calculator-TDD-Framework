namespace CrossPlatformCalculator.Core.Configuration;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> LazyInstance = new(() => new AppConfiguration());

    private AppConfiguration()
    {
    }

    public static AppConfiguration Instance => LazyInstance.Value;

    public string ApplicationName => "CrossPlatform Calculator TDD Framework";

    public string Architecture => "API-first layered MVC";

    public IReadOnlyList<string> SupportedPlatforms { get; } = ["web", "mobile", "desktop"];

    public IReadOnlyList<string> SupportedOperations { get; } = ["+", "-", "×", "÷", "%", "√", "x²", "1/x"];
}
