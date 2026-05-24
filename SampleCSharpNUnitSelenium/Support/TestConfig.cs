namespace SampleCSharpNUnitSelenium.Support;

/// <summary>
/// Thread-safe Immutable configuration for test execution.
/// </summary>
public sealed class TestConfig
{
    public required string BaseUrl { get; init; }
    public required string BrowserType { get; init; }
    public string? RemoteUrl { get; init; }
    public bool Headless { get; init; }
    public string? LoginUsername { get; init; }
    public string? LoginPassword { get; init; }

    public bool IsLocal =>
        string.IsNullOrWhiteSpace(RemoteUrl);
}
