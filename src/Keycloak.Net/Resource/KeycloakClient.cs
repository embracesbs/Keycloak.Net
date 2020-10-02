using Flurl.Http;
using Keycloak.Net.Models.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        public async Task<bool> CreateResourceAsync(string realm, string resourceServerId, Resource resource)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
                .PostJsonAsync(resource)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Resource>> GetResourcesAsync(string realm, string resourceServerId = null, 
            bool deep = false, int? first = null, int? max = null, string name = null, string owner = null,
            string type = null, string uri = null)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(deep)] = deep,
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(name)] = name,
                [nameof(owner)] = owner,
                [nameof(type)] = type,
                [nameof(uri)] = uri
            };
            
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<Resource>>()
                .ConfigureAwait(false);
        }

        public async Task<Resource> GetResourceAsync(string realm, string resourceServerId, string resourceId) => await GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
            .GetJsonAsync<Resource>()
            .ConfigureAwait(false);

        public async Task<bool> UpdateResourceAsync(string realm, string resourceServerId, string resourceId, Resource resource)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
                .PutJsonAsync(resource)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteResourceAsync(string realm, string resourceServerId, string resourceId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
                .DeleteAsync()
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
