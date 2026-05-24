namespace SampleCSharpNUnitSelenium.PageObjects.Login
{
    /// <summary>
    /// Login page object
    /// </summary>
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly TestConfig _config;

        // Elements
        private readonly By _loginPageHeading = By.XPath("//h2[contains(text(),'Login Page')]");
        private readonly By _usernameInput = By.CssSelector("#username");
        private readonly By _passwordInput = By.CssSelector("#password");
        private readonly By _submitButton = By.CssSelector("#login > button > i");
        private readonly By _loginError = By.CssSelector("#flash");

        public LoginPage(IWebDriver driver, TestConfig config)
        {
            _driver = driver;
            _config = config;
        }

        // (Thread safe) Element Method
        public IWebElement LoginPageHeading() => _driver.WaitAndFind(_loginPageHeading);
        public IWebElement UsernameInput() => _driver.WaitAndFind(_usernameInput);
        public IWebElement PasswordInput() => _driver.WaitAndFind(_passwordInput);
        public IWebElement SubmitButton() => _driver.WaitAndFind(_submitButton);
        public IWebElement LoginError() => _driver.WaitAndFind(_loginError);

        /// <summary>
        /// Login with supplied credentials
        /// </summary>
        public void LoginUser(string username, string password)
        {
            // 3. The elements are automatically waited on when accessed via the methods!
            UsernameInput().SendKeys(username);
            PasswordInput().SendKeys(password);
            SubmitButton().Click();
        }

        /// <summary>
        /// Login using valid credentials
        /// </summary>
        public void LoginWithValidCredentials()
        {
            if (string.IsNullOrEmpty(_config.LoginUsername) ||
                string.IsNullOrEmpty(_config.LoginPassword))
            {
                throw new InvalidOperationException(
                    "LOGIN_USERNAME and LOGIN_PASSWORD must be configured");
            }

            LoginUser(_config.LoginUsername, _config.LoginPassword);
        }
    }
}
