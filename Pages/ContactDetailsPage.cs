using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AtlantTask.Pages
{
    public class ContactDetailsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ContactDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }

        private By FirstNameLocator => By.XPath("//span[@id='firstName']");
        private By LastNameLocator => By.XPath("//span[@id='lastName']");

        public string DisplayedFirstName
        {
            get
            {
                var firstNameElement = _wait.Until(ExpectedConditions.ElementIsVisible(FirstNameLocator));
                return firstNameElement.Text;
            }
        }

        public string DisplayedLastName
        {
            get
            {
                var lastNameElement = _wait.Until(ExpectedConditions.ElementIsVisible(LastNameLocator));
                return lastNameElement.Text;
            }
        }

    }

}
