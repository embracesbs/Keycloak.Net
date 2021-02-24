using Flurl.Http;
using Keycloak.Net.Models.AuthorizationPermissions;
using Keycloak.Net.Models.AuthorizationScopes;
using Keycloak.Net.Models.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorizationResource = Keycloak.Net.Models.AuthorizationResources.AuthorizationResource;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        #region Permissions
        public async Task<AuthorizationPermission> CreateAuthorizationPermissionAsync(string realm, string clientId, AuthorizationPermission permission) =>
            await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .AppendPathSegment(permission.Type == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
                .PostJsonAsync(permission)
                .ReceiveJson<AuthorizationPermission>()
                .ConfigureAwait(false);

        public async Task<AuthorizationPermission> GetAuthorizationPermissionByIdAsync(string realm, string clientId,
            AuthorizationPermissionType permissionType, string permissionId) => await GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
            .AppendPathSegment(permissionType == AuthorizationPermissionType.Scope ? "/scope" : "/resource")
            .AppendPathSegment($"/{permissionId}")
            .GetJsonAsync<AuthorizationPermission>()
            .ConfigureAwait(false);

        public async Task<IEnumerable<AuthorizationPermission>> GetAuthorizationPermissionsAsync(string realm, string clientId, AuthorizationPermissionType? ofPermissionType = null, 
            int? first = null, int? max = null, string name = null, string resource = null, string scope = null)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(name)] = name,
                [nameof(resource)] = resource,
                [nameof(scope)] = scope
            };

            var request = GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission");

            if (ofPermissionType.HasValue)
                request.AppendPathSegment(ofPermissionType.Value == AuthorizationPermissionType.Scope ? "/scope" : "/resource");
            
            return await request
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<AuthorizationPermission>>()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAuthorizationPermissionAsync(string realm, string clientId, AuthorizationPermission permission)
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
        
        public async Task<IEnumerable<Policy>> GetAuthorizationPermissionAssociatedPoliciesAsync(string realm, string clientId, string permissionId)
        {
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{permissionId}/associatedPolicies")
                .GetJsonAsync<IEnumerable<Policy>>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<AuthorizationScope>> GetAuthorizationPermissionAssociatedScopesAsync(string realm, string clientId, string permissionId)
        {
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{permissionId}/scopes")
                .GetJsonAsync<IEnumerable<AuthorizationScope>>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<AuthorizationResource>> GetAuthorizationPermissionAssociatedResourcesAsync(string realm, string clientId, string permissionId)
        {
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{permissionId}/resources")
                .GetJsonAsync<IEnumerable<AuthorizationResource>>()
                .ConfigureAwait(false);
        }
        #endregion

        #region Policy

        public async Task<IEnumerable<Policy>> GetAuthorizationPoliciesAsync(string realm, string clientId,
            int? first = null, int? max = null,
            string name = null, string resource = null,
            string scope = null, bool? permission = null)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(name)] = name,
                [nameof(resource)] = resource,
                [nameof(scope)] = scope,
                [nameof(permission)] = permission
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<Policy>>()
                .ConfigureAwait(false);
        }

        public async Task<RolePolicy> CreateRolePolicyAsync(string realm, string clientId, RolePolicy policy)
        {
            return await CreatePolicyAsync<RolePolicy>(realm, clientId, policy, PolicyType.Role, "role").ConfigureAwait(false);
        }

        public async Task<RolePolicy> GetRolePolicyByIdAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId)
        {
            return await GetPolicyByIdAsync<RolePolicy>(realm, clientId, policyType, rolePolicyId, PolicyType.Role, "role").ConfigureAwait(false);
        }

        public async Task<IEnumerable<RolePolicy>> GetRolePoliciesAsync(string realm, string clientId, 
            int? first = null, int? max = null, 
            string name = null, string resource = null,
            string scope = null, bool? permission = null)
        {
            return await GetPoliciesAsync<RolePolicy>(realm, clientId, first, max, name, resource, scope, permission, "role")
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateRolePolicyAsync(string realm, string clientId, RolePolicy policy)
        {
            return await UpdatePolicy(realm, clientId, policy, PolicyType.Role, "role")
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteRolePolicyAsync(string realm, string clientId, PolicyType policyType, string rolePolicyId)
        {
            return await DeletePolicyAsync(realm, clientId, policyType, rolePolicyId, PolicyType.Role, "role")
                .ConfigureAwait(false);
        }

        public async Task<GroupPolicy> CreateGroupPolicyAsync(string realm, string clientId, GroupPolicy policy)
        {
            return await CreatePolicyAsync<GroupPolicy>(realm, clientId, policy, PolicyType.Group, "group").ConfigureAwait(false);
        }

        public async Task<GroupPolicy> GetGroupPolicyByIdAsync(string realm, string clientId, PolicyType policyType, string groupPolicyId)
        {
            return await GetPolicyByIdAsync<GroupPolicy>(realm, clientId, policyType, groupPolicyId, PolicyType.Group, "group").ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPolicy>> GetGroupPoliciesAsync(string realm, string clientId,
            int? first = null, int? max = null,
            string name = null, string resource = null,
            string scope = null, bool? permission = null)
        {
            return await GetPoliciesAsync<GroupPolicy>(realm, clientId, first, max, name, resource, scope, permission, "group")
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateGroupPolicyAsync(string realm, string clientId, GroupPolicy policy)
        {
            return await UpdatePolicy(realm, clientId, policy, PolicyType.Group, "group")
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteGroupPolicyAsync(string realm, string clientId, PolicyType policyType, string groupPolicyId)
        {
            return await DeletePolicyAsync(realm, clientId, policyType, groupPolicyId, PolicyType.Group, "group")
                .ConfigureAwait(false);
        }

        private async Task<T> GetPolicyByIdAsync<T>(string realm, string clientId, PolicyType policyType, string policyId, PolicyType expectedPolicyType, string path)
        {
            return await GetBaseUrl(realm)
              .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
              .AppendPathSegment(policyType == expectedPolicyType ? $"/{path}" : string.Empty)
              .AppendPathSegment($"/{policyId}")
              .GetJsonAsync<T>()
              .ConfigureAwait(false);
        }

        private async Task<T> CreatePolicyAsync<T>(string realm, string clientId, T policy, PolicyType policyType, string path)
            where T : PolicyBase
        {
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(policy.Type == policyType ? $"/{path}" : string.Empty)
                .PostJsonAsync(policy)
                .ReceiveJson<T>()
                .ConfigureAwait(false);
        }

        private async Task<IEnumerable<T>> GetPoliciesAsync<T>(string realm, string clientId, int? first, int? max, string name, string resource, string scope, bool? permission, string typeName)
            where T : PolicyBase
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(name)] = name,
                [nameof(resource)] = resource,
                [nameof(scope)] = scope,
                [nameof(permission)] = permission
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{typeName}")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<T>>()
                .ConfigureAwait(false);
        }

        private async Task<bool> UpdatePolicy<T>(string realm, string clientId, T policy, PolicyType type, string path)
            where T: PolicyBase
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(policy.Type == type ? $"/{path}" : string.Empty)
                .AppendPathSegment($"/{policy.Id}")
                .PutJsonAsync(policy)
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> DeletePolicyAsync(
            string realm, 
            string clientId, 
            PolicyType policyType, 
            string policyId, 
            PolicyType type, string path)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy")
                .AppendPathSegment(policyType == type ? $"/{path}" : string.Empty)
                .AppendPathSegment($"/{policyId}")
                .DeleteAsync()
                .ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
