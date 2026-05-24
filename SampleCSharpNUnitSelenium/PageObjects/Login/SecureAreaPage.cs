namespace SampleCSharpNUnitSelenium.PageObjects.SecureArea
{
    public class SecureAreaPage
    {
        /// <summary>
        /// Secure Area (logged in) page object
        /// </summary>
        private readonly IWebDriver _driver;

        // Elements
        private readonly By _secureAreaHeading = By.XPath("//h2[contains(text(),'Secure Area')]");
        private readonly By _loggedinFlash = By.XPath("//*[@id='flash']");
        private readonly By _loggedInMessage = By.XPath("//*[contains(text(),'logged in')]");
        private readonly By _logoutButton = By.XPath("//*[@id='content']/div/a");

        public SecureAreaPage(IWebDriver driver) => _driver = driver;

        // (Thread safe) Element Method
        public IWebElement SecureAreaHeading() => _driver.WaitAndFind(_secureAreaHeading);
        public IWebElement LoggedinFlash() => _driver.WaitAndFind(_loggedinFlash);
        public IWebElement LoggedInMessage() => _driver.WaitAndFind(_loggedInMessage);
        public IWebElement LogoutButton() => _driver.WaitAndFind(_logoutButton);
    }
}
