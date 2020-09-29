using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Models.Clients;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        #region Permissions
        public async Task<bool> CreateAuthorizationPermissionAsync(string realm, string clientId, AuthorizationPermission permission)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .AppendPathSegment(permission.Type == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
                .PostJsonAsync(permission)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<AuthorizationPermission> GetAuthorizationPermissionByIdAsync(string realm, string clientId,
            AuthorizationPermissionType permissionType, string permissionId) => await GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
            .AppendPathSegment(permissionType == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
            .AppendPathSegment($"/{permissionId}")
            .GetJsonAsync<AuthorizationPermission>()
            .ConfigureAwait(false);

        public async Task<IEnumerable<AuthorizationPermission>> GetAuthorizationPermissionsAsync(string realm, string clientId, int? first = null,
            int? max = null)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<AuthorizationPermission>>()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAuthorizationPermissionAsync(string realm, string clientId, string roleName, AuthorizationPermission permission)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .AppendPathSegment(permission.Type == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
                .AppendPathSegment($"/{permission.Id}")
                .PutJsonAsync(permission)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAuthorizationPermissionAsync(string realm, string clientId, AuthorizationPermissionType permissionType,
            string permissionId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .AppendPathSegment(permissionType == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
                .AppendPathSegment($"/{permissionId}")
                .DeleteAsync()
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        #endregion 

        #region Policy
        public async Task<bool> CreateRolePolicyAsync(string realm, string clientId, RolePolicy policy)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(policy.Type == PolicyType.Role ? "/role" : string.Empty)
                .PostJsonAsync(policy)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<RolePolicy> GetRolePolicyByIdAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId) => await GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
            .AppendPathSegment(policyType == PolicyType.Role ? "/role" : string.Empty)
            .AppendPathSegment($"/{rolePolicyId}")
            .GetJsonAsync<RolePolicy>()
            .ConfigureAwait(false);

        public async Task<IEnumerable<RolePolicy>> GetRolePolicyAsync(string realm, string clientId, int? first = null,
            int? max = null, bool? permission = null)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(permission)] = permission
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<RolePolicy>>()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateRolePolicyAsync(string realm, string clientId, string roleName, RolePolicy permission)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(permission.Type == PolicyType.Role ? "/role" : String.Empty)
                .AppendPathSegment($"/{permission.Id}")
                .PutJsonAsync(permission)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRolePolicyAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(policyType == PolicyType.Role ? "/role" : string.Empty)
                .AppendPathSegment($"/{rolePolicyId}")
                .DeleteAsync()
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
