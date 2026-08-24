using CrossPlatformCalculator.Core.Factories;

namespace CrossPlatformCalculator.Core.Tests;

public class PlatformUiFactoryProviderTests
{
    private readonly PlatformUiFactoryProvider _provider = new();

    [Theory]
    [InlineData("web", "grid-web", "icon-plus-web")]
    [InlineData("mobile", "stack-mobile", "icon-plus-mobile")]
    [InlineData("desktop", "dock-desktop", "icon-plus-desktop")]
    public void GetFactory_returns_platform_specific_assets(string platform, string expectedLayout, string expectedPlusIcon)
    {
        var factory = _provider.GetFactory(platform);
        var layout = factory.CreateLayout();
        var icons = factory.CreateIcons();

        Assert.Equal(expectedLayout, layout.LayoutId);
        Assert.Equal(expectedPlusIcon, icons["+"]);
    }

    [Fact]
    public void GetFactory_throws_for_unknown_platform()
    {
        Assert.Throws<NotSupportedException>(() => _provider.GetFactory("console"));
    }
}
