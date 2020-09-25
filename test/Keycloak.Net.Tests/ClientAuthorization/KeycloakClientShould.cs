using System.Linq;
using System.Threading.Tasks;
using Keycloak.Net.Models.Clients;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {

        [Theory(Skip = "Not working yet")]
        [InlineData("Insurance", "insurance-product", AuthorizationPermissionType.Resource, "permission-id")]
        public async Task GetAuthorizationPermissionByIdAsync(string realm, string clientId, AuthorizationPermissionType type, string permissionId)
        {
            var result = await _client.GetAuthorizationPermissionByIdAsync(realm, clientId, type, permissionId);
            Assert.NotNull(result);
        }

        [Theory(Skip = "Not working yet")]
        [InlineData("Insurance", "insurance-product")]
        public async Task GetAuthorizationPermissionsAsync(string realm, string clientId)
        {
            var result = await _client.GetAuthorizationPermissionsAsync(realm, clientId);
            Assert.NotNull(result);
        }
    }
}
