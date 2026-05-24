using OpenQA.Selenium.Support.UI;

namespace SampleCSharpNUnitSelenium.Support
{
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Safely waits for an element to be visible and enabled before returning it.
        /// </summary>
        public static IWebElement WaitAndFind(this IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }
    }
}
