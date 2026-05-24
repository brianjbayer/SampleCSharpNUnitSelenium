using SampleCSharpNUnitSelenium.PageObjects.Login;
using SampleCSharpNUnitSelenium.PageObjects.SecureArea;

namespace SampleCSharpNUnitSelenium.Tests.Login
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class ValidLoginTests : LoginTestBase
    {
        [Test]
        public void TestLoginWithValidCredentials()
        {
            var loginPage = new LoginPage(Driver, Config);
            loginPage.LoginWithValidCredentials();

            var secureArea = new SecureAreaPage(Driver);

            try
            {
                _ = secureArea.SecureAreaHeading().Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Secure area page failed to load within the timeout period.");
            }

            secureArea.LoggedInMessage().Displayed.ShouldBeTrue();
        }
    }
}
