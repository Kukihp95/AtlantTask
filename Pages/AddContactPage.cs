using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AtlantTask.Pages
{
    public class AddContactPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private static readonly Random _random = new Random();

        public AddContactPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        private IWebElement FirstNameInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("firstName")));
        private IWebElement LastNameInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("lastName")));
        private IWebElement DateOfBirthInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("birthdate")));
        private IWebElement EmailInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
        private IWebElement PhoneInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("phone")));
        private IWebElement StreetAddress1Input => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("street1")));
        private IWebElement StreetAddress2Input => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("street2")));
        private IWebElement CityInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("city")));
        private IWebElement StateOrProvinceInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("stateProvince")));
        private IWebElement PostalCodeInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("postalCode")));
        private IWebElement CountryInput => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("country")));
        private IWebElement SubmitButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Submit']")));
        private IWebElement CancelButton => _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Cancel']")));

        private static string GenerateRandomName()
        {
            string[] names = { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Helen", "Isaac", "Jack" };
            return names[_random.Next(names.Length)];
        }

        public void AddContact(
            string firstName,
            string lastName,
            string dateOfBirth,
            string email,
            string phone,
            string streetAddress1,
            string streetAddress2,
            string city,
            string stateOrProvince,
            string postalCode,
            string country)
        {
            FirstNameInput.SendKeys(firstName);
            LastNameInput.SendKeys(lastName);
            DateOfBirthInput.SendKeys(dateOfBirth);
            EmailInput.SendKeys(email);
            PhoneInput.SendKeys(phone);
            StreetAddress1Input.SendKeys(streetAddress1);
            StreetAddress2Input.SendKeys(streetAddress2);
            CityInput.SendKeys(city);
            StateOrProvinceInput.SendKeys(stateOrProvince);
            PostalCodeInput.SendKeys(postalCode);
            CountryInput.SendKeys(country);

            SubmitButton.Click();
        }

        public string EditContactFirstName()
        {
            string newFirstName = GenerateRandomName();
            Thread.Sleep(1000);
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(newFirstName);
            SubmitButton.Click();
            return newFirstName;
        }

        public void Cancel()
        {
            CancelButton.Click();
        }
    }
}
