using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;

namespace SampleCSharpNUnitSelenium.Support
{
    public class BrowserFactory
    {
        /// <summary>
        /// Creates Selenium Browser
        /// </summary>
        public const int StandardWaitSecs = 10;
        public const int MaxHDWidth = 1875;
        public const int MaxHDHeight = 1080;

        // Options builders
        private static T ConfigureChromiumOptions<T>(bool isHeadless)
            where T : ChromiumOptions, new()
        {
            /// <summary>
            /// Custom Chromium Options (Chrome, Edge)
            /// </summary>
            var opts = new T();

            opts.AddUserProfilePreference("credentials_enable_service", false);
            opts.AddUserProfilePreference("profile.password_manager_enabled", false);

            opts.AddExcludedArgument("enable-automation");
            opts.AddAdditionalOption("useAutomationExtension", false);

            if (isHeadless)
            {
                opts.AddArgument("--headless=new");
            }

            return opts;
        }

        private static FirefoxOptions ConfigureFirefoxOptions(bool isHeadless)
        {
            /// <summary>
            /// Custom Firefox Options
            /// </summary>
            var opts = new FirefoxOptions();

            if (isHeadless)
            {
                opts.AddArgument("--headless");
            }

            return opts;
        }

        // Driver creation
        private static IWebDriver CreateBrowser<T, U>(
            T options,
            bool isLocal,
            string? remoteUrl)
            where T : DriverOptions
            where U : WebDriver
        {
            if (!isLocal && string.IsNullOrWhiteSpace(remoteUrl))
            {
                throw new InvalidOperationException(
                    "REMOTE_URL must be set for remote execution."
                );
            }

            if (isLocal)
            {
                return (U)(Activator.CreateInstance(typeof(U), options)
                    ?? throw new InvalidOperationException(
                        $"Failed to create local driver {typeof(U).Name}"
                    ));
            }

            return new RemoteWebDriver(new Uri(remoteUrl!), options);
        }

        // Entry point
        public static IWebDriver GetBrowser(TestConfig config)
        {
            var isHeadless = config.Headless;
            var isLocal = config.IsLocal;
            var browser = config.BrowserType.ToLowerInvariant();
            var remoteUrl = config.RemoteUrl;

            IWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    {
                        var options = ConfigureChromiumOptions<ChromeOptions>(isHeadless);
                        driver = CreateBrowser<ChromeOptions, ChromeDriver>(options, isLocal, remoteUrl);
                        break;
                    }

                case "edge":
                    {
                        var options = ConfigureChromiumOptions<EdgeOptions>(isHeadless);
                        driver = CreateBrowser<EdgeOptions, EdgeDriver>(options, isLocal, remoteUrl);
                        break;
                    }

                case "firefox":
                    {
                        var options = ConfigureFirefoxOptions(isHeadless);
                        driver = CreateBrowser<FirefoxOptions, FirefoxDriver>(options, isLocal, remoteUrl);
                        break;
                    }

                case "safari":
                    {
                        driver = new SafariDriver();

                        // Safari startup stability delay
                        Thread.Sleep(1000);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(browser),
                        $"Unsupported browser: {browser}"
                    );
            }

            // Standardized post-init setup
            driver.Navigate().GoToUrl("about:blank");

            driver.Manage().Window.Size =
                new System.Drawing.Size(MaxHDWidth, MaxHDHeight);

            driver.Manage().Cookies.DeleteAllCookies();

            return driver;
        }
    }
}
