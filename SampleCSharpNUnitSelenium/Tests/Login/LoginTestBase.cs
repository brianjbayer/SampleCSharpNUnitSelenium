using SampleCSharpNUnitSelenium.PageObjects.Login;

namespace SampleCSharpNUnitSelenium.Tests.Login
{
    /// <summary>
    /// Base class for login tests, handles navigation and loading synchronization.
    /// </summary>
    public abstract class LoginTestBase : Base
    {
        [SetUp]
        public void SetUpTest()
        {
            var loginUrl = new Uri(new Uri(Config.BaseUrl), "login");

            Driver.Navigate().GoToUrl(loginUrl);

            WaitForLoginPageToLoad();
        }

        [TearDown]
        public void TearDownTest()
        {
            // Base.TearDown() handles browser destruction safely
        }

        private void WaitForLoginPageToLoad()
        {
            try
            {
                // Thread safe local instantiation
                var loginPage = new LoginPage(Driver, Config);
                _ = loginPage.LoginPageHeading().Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Login page not loaded: Heading element timed out.");
            }
        }
    }
}
