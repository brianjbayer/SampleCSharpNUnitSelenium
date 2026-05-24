
// Default parallel level
[assembly: LevelOfParallelism(4)]

namespace SampleCSharpNUnitSelenium.Tests
{
    public abstract class Base
    {
        [ThreadStatic]
        private static IWebDriver? _threadDriver;

        protected TestConfig Config = null!;

        protected IWebDriver Driver
        {
            get => _threadDriver ?? throw new InvalidOperationException("WebDriver is not initialized.");
            private set => _threadDriver = value;
        }

        [SetUp]
        public void SetUp()
        {
            Config = ConfigLoader.LoadConfig();

            Driver = BrowserFactory.GetBrowser(Config);

            var threadId = Environment.CurrentManagedThreadId;
            var testName = TestContext.CurrentContext.Test.Name;

            TestContext.Progress.WriteLine($"Running test '{testName}' on Thread ID: {threadId}");
        }

        [TearDown]
        public void TearDown()
        {
            // 4. Calling Driver directly here satisfies the NUnit1032 compiler analyzer rule
            if (_threadDriver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                _threadDriver = null;
            }
        }
    }
}
