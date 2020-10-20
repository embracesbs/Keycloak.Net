using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        [Theory]
        [Trait("Category", "EmbraceTests")]
        [InlineData("master")]
        public async Task CreateUserAsync(string realm)
        {
            var user = new Models.Users.User()
            {
                UserName = "testun1",
                Enabled = true,
                EmailVerified = true,
                FirstName = "testfirstname1",
                LastName = "testlastname1",
                Email = "testemail@e.ee"
            };

            var result = await _client.CreateAndRetrieveUserIdAsync(realm, user);
            Assert.False(string.IsNullOrWhiteSpace(result), "Created user ID not returned");
        }

        [Theory]
        [Trait("Category", "EmbraceTests")]
        [InlineData("master")]
        public async Task CreateClientAndRetrieveClientIdAsync(string realm)
        {
            var client = new Models.Clients.Client()
            {
                Name = "testclient1",
                Enabled = true,
                BaseUrl = "baseurl1",
            };

            var result = await _client.CreateClientAndRetrieveClientIdAsync(realm, client);
            Assert.False(string.IsNullOrWhiteSpace(result), "Created client ID not returned");
        }

        [Theory]
        [Trait("Category", "EmbraceTests")]
        [InlineData("master")]
        public async Task GetOpenIDConfigurationAsync(string realm)
        {
            var result = await _client.GetOpenIDConfigurationAsync(realm);
            Assert.EndsWith(realm, result.Issuer.ToString());
        }
    }
}
