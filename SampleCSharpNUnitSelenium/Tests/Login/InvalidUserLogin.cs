using SampleCSharpNUnitSelenium.PageObjects.Login;

namespace SampleCSharpNUnitSelenium.Tests.Login
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class InvalidLoginTests : LoginTestBase
    {
        [Test]
        public void TestLoginWithInvalidUsernameAndPasswordAnd_ShowsUsernameError()
        {
            // Thread safe local instantiation
            var loginPage = new LoginPage(Driver, Config);

            loginPage.LoginUser("InvalidUsername", "InvalidPassword");

            try
            {
                _ = loginPage.LoginError().Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Login error flash message was not displayed.");
            }

            loginPage.LoginError().Text.ShouldContain("Your username is invalid!");
        }

        [Test]
        public void TestLoginWithValidUsernameAndInvalidPassword_ShowsPasswordError()
        {
            var loginPage = new LoginPage(Driver, Config);

            loginPage.LoginUser(Config.LoginUsername!, "InvalidPassword");

            try
            {
                _ = loginPage.LoginError().Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Login error flash message was not displayed.");
            }

            loginPage.LoginError().Text.ShouldContain("Your password is invalid!");
        }
    }
}
