using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using RestSharp;

namespace AtlantTask
{
    [TestFixture]
    public class ContactListApiTests
    {
        private string baseUrl = "https://thinking-tester-contact-list.herokuapp.com";
        private string authToken;
        private string contactId;


        [SetUp]
        public void SetUp()
        {
            authToken = GetAuthToken();
        }

        [Test, Order(1)]
        public void GetContactList()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/contacts", Method.Get);
            request.AddHeader("Authorization", $"Bearer {authToken}");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ClassicAssert.IsNotNull(response.Content);
            var contacts = JsonConvert.DeserializeObject<JArray>(response.Content);
            ClassicAssert.Greater(contacts.Count, 0);
        }

        [Test, Order(2)]
        public void AddContact()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/contacts", Method.Post);
            request.AddHeader("Authorization", $"Bearer {authToken}");
            request.AddHeader("Content-Type", "application/json");

            var contactData = new
            {
                firstName = "John",
                lastName = "Doe",
                birthdate = "1970-01-01",
                email = "jdoe@fake.com",
                phone = "8005555555",
                street1 = "1 Main St.",
                street2 = "Apartment A",
                city = "Anytown",
                stateProvince = "KS",
                postalCode = "12345",
                country = "USA"
            };

            request.AddJsonBody(contactData);
            var response = client.Execute(request);
            ClassicAssert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var responseData = JsonConvert.DeserializeObject<JObject>(response.Content);
            ClassicAssert.IsTrue(responseData.ContainsKey("_id"));
            contactId = responseData["_id"].ToString();

        }

        [Test, Order(3)]
        public void UpdateContact()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"/contacts/{contactId}", Method.Put);
            request.AddHeader("Authorization", $"Bearer {authToken}");
            request.AddHeader("Content-Type", "application/json");

            var updateData = new
            {
                firstName = "Amy",
                lastName = "Smith",
                birthdate = "1971-01-01",
                email = "amysmith@fake.com",
                phone = "8004445555",
                street1 = "1 Main St.",
                street2 = "Apartment A",
                city = "Anytown",
                stateProvince = "KS",
                postalCode = "12345",
                country = "BiH"
            };

            request.AddJsonBody(updateData);
            var response = client.Execute(request);
            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(4)]
        public void DeleteContact()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest($"/contacts/{contactId}", Method.Delete);
            request.AddHeader("Authorization", $"Bearer {authToken}");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private string GetAuthToken()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/users/login", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var loginData = new
            {
                email = "amirmrco@hotmail.com",
                password = "1234567"
            };

            request.AddJsonBody(loginData);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<JObject>(response.Content);
                return responseData["token"]?.ToString();
            }
            else
            {
                throw new Exception($"Login failed: {response.StatusCode} - {response.Content}");
            }
        }
    }
}