using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace AtlantTask.Pages
{
    public class ContactListPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ContactListPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); 
        }

        public bool IsAtContactListPage()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Add a New Contact']"))).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false; 
            }
            catch (NoSuchElementException)
            {
                return false; 
            }
        }

        public void ClickAddContact()
        {
            try
            {
                var addContactButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Add a New Contact']")));
                addContactButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Add Contact button not found or not clickable.");
            }
        }

        public bool IsContactPresent(string firstName, string lastName)
        {
            try
            {
                var contactTable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//table[@id='myTable']")));

                var rows = contactTable.FindElements(By.TagName("tr")).Skip(1);

                foreach (var row in rows)
                {
                    var cells = row.FindElements(By.TagName("td"));
                    if (cells.Count > 1) // Ensure row has data
                    {
                        string fullName = $"{cells[0].Text} {cells[1].Text}";
                        if (fullName.Contains(firstName) && fullName.Contains(lastName))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false; 
            }
            catch (NoSuchElementException)
            {
                return false; 
            }
        }

        public void ClickFirstContact()
        {
            var contactTable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//table[@id='myTable']")));


            var rows = contactTable.FindElements(By.TagName("tr")).Skip(1).ToList();
            if (rows.Count > 0)
            {
                rows[0].Click();
            }
        }

        public void ClickEditContact()
        {
            var editContactButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Edit Contact']")));
            editContactButton.Click();
        }

        public void ClickDeleteContact()
        {
            var deleteContactButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Delete Contact']")));
            deleteContactButton.Click();

            var alert = _wait.Until(driver => driver.SwitchTo().Alert());
            alert.Accept();
        }
    }

}
