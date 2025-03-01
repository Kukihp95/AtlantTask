using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AtlantTask.Pages
{
    public class SignUpPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SignUpPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); 
        }

        private IWebElement FirstNameInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("firstName")));
        private IWebElement LastNameInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("lastName")));
        private IWebElement EmailInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
        private IWebElement PasswordInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
        private IWebElement SignUpButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Sign Up']")));
        private IWebElement SignUpSubmitButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Submit']")));

        public void SignUp(string firstName, string lastName, string email, string password)
        {
            FirstNameInput.SendKeys(firstName);
            LastNameInput.SendKeys(lastName);
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignUpSubmitButton.Click();
        }
    }
}
