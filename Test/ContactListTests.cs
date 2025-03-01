using NUnit.Framework;
using AtlantTask.Pages;
using NUnit.Framework.Legacy;

namespace AtlantTask
{
    [TestFixture]
    public class ContactListTests : BaseTest
    {
        private string testEmail = "existing_user1@example.com";
        private string testPassword = "Password123";

        [Test, Order(1)]
        public void SignUpIfNeeded_ThenLogin()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Login(testEmail, testPassword);
            Thread.Sleep(1000);

            var contactListPage = new ContactListPage(Driver);
            if (!contactListPage.IsAtContactListPage())
            {
                loginPage.GoToSignUp();
                var signUpPage = new SignUpPage(Driver);
                signUpPage.SignUp("Existing", "User", testEmail, testPassword);
            }
            ClassicAssert.IsTrue(contactListPage.IsAtContactListPage(), "Contact List page should be displayed.");
        }

        [Test, Order(2)]
        public void AddContactAndVerify()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Login(testEmail, testPassword);

            var contactListPage = new ContactListPage(Driver);
            contactListPage.ClickAddContact();

            var addContactPage = new AddContactPage(Driver);
            string firstName = "Edin", lastName = "Dzeko";
            string dateOfBirth = "1995-01-12";
            string email = "john.doe" + DateTime.Now.Ticks + "@example.com";
            string phone = "123456789";
            string street1 = "123 Main St";
            string street2 = "Apt 4B";
            string city = "New York";
            string stateProvince = "NY";
            string postalCode = "10001";
            string country = "USA";

            addContactPage.AddContact(firstName, lastName,dateOfBirth, email, phone, street1, street2, city, stateProvince, postalCode, country);

            ClassicAssert.IsTrue(contactListPage.IsContactPresent(firstName, lastName), "New contact should appear in the list.");


            contactListPage.ClickFirstContact();

            contactListPage.ClickEditContact();

            string newFirstName = addContactPage.EditContactFirstName();


            var contactDetailsPage = new ContactDetailsPage(Driver);

            string displayedFirstName = contactDetailsPage.DisplayedFirstName;
            string displayedLastName = contactDetailsPage.DisplayedLastName;

            // Assert that the displayed names match the expected ones
            ClassicAssert.IsTrue(displayedFirstName.Equals(newFirstName) && displayedLastName.Equals(lastName),
            $"Contact name should be updated. Expected: {newFirstName} {lastName}, but found: {displayedFirstName} {displayedLastName}");

            contactListPage.ClickDeleteContact();

            ClassicAssert.IsFalse(contactListPage.IsContactPresent(newFirstName, lastName), "Contact should be deleted from the list.");
        }
    }
}
