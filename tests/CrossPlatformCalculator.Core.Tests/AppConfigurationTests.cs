using CrossPlatformCalculator.Core.Configuration;

namespace CrossPlatformCalculator.Core.Tests;

public class AppConfigurationTests
{
    [Fact]
    public void Instance_returns_singleton_configuration()
    {
        var first = AppConfiguration.Instance;
        var second = AppConfiguration.Instance;

        Assert.Same(first, second);
        Assert.Contains("web", first.SupportedPlatforms);
        Assert.Contains("mobile", first.SupportedPlatforms);
        Assert.Contains("desktop", first.SupportedPlatforms);
    }
}
