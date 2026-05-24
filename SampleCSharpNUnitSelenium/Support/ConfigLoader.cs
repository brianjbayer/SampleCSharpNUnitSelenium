using Microsoft.Extensions.Configuration;

namespace SampleCSharpNUnitSelenium.Support;
/// <summary>
/// Loads test configuration with precedence:
/// 1. environment variables,
/// 2. ASPNETCORE_ENVIRONMENT
/// 3. appsettings.json
/// </summary>
public static class ConfigLoader
{
    public static TestConfig LoadConfig()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        if (!string.IsNullOrEmpty(env))
        {
            builder.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
        }

        var configRoot = builder.AddEnvironmentVariables().Build();

        return new TestConfig
        {
            BaseUrl = configRoot["BASE_URL"]
                ?? throw new InvalidOperationException("BASE_URL missing"),

            BrowserType = configRoot["BROWSER_TYPE"]
                ?? throw new InvalidOperationException("BROWSER_TYPE missing"),

            RemoteUrl = configRoot["REMOTE_URL"],

            Headless = configRoot["HEADLESS"] != null,

            LoginUsername = configRoot["LOGIN_USERNAME"],

            LoginPassword = configRoot["LOGIN_PASSWORD"]
        };
    }
}
