using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AtlantTask.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        private IWebElement EmailInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
        private IWebElement PasswordInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
        private IWebElement LoginButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Submit']")));
        private IWebElement SignUpLink => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Sign up']")));

        public void EnterEmail(string email) => EmailInput.SendKeys(email);
        public void EnterPassword(string password) => PasswordInput.SendKeys(password);
        public void ClickLogin() => LoginButton.Click();
        public void GoToSignUp() => SignUpLink.Click();

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickLogin();
        }
    }
}
