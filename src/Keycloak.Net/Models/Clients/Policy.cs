using System.Collections.Generic;
using Keycloak.Net.Common.Converters;
using Keycloak.Net.Models.AuthorizationPermissions;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Clients
{
    public class PolicyBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonConverter(typeof(PolicyTypeConverter))]
        public PolicyType Type { get; set; }

        [JsonConverter(typeof(PolicyDecisionLogicConverter))]
        public PolicyDecisionLogic Logic { get; set; }

        [JsonConverter(typeof(DecisionStrategiesConverter))]
        public DecisionStrategy DecisionStrategy { get; set; }
    }

    public class Policy: PolicyBase
    {
        [JsonProperty("config")]
        public PolicyConfig Config { get; set; }
    }

    public class RolePolicy: PolicyBase
    {
        public RolePolicy()
        {
            Type = PolicyType.Role;
        }

        [JsonProperty("roles")]
        public IEnumerable<RoleConfig> RoleConfigs { get; set; }
    }

    public class GroupPolicy: PolicyBase
    {
        public GroupPolicy()
        {
            Type = PolicyType.Group;
        }

        [JsonProperty("groups")]
        public IEnumerable<GroupConfig> GroupConfigs { get; set; }
    }

    public class UserPolicy: PolicyBase
    {
        public UserPolicy()
        {
            Type = PolicyType.User;
        }

        [JsonProperty("users")]
        public IEnumerable<string> UserIds { get; set; }
    }

    public class RoleConfig
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }
    }

    public class GroupConfig
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("extendChildren")]
        public bool ExtendChildren { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public enum PolicyType
    {
        Role,
        Client,
        Time,
        User,
        Aggregate,
        Group,
        Js
    }

    public class PolicyConfig
    {
        [JsonProperty("roles")]
        public IEnumerable<RoleConfig> RoleConfigs { get; set; }

        [JsonProperty("groups")]
        public IEnumerable<GroupConfig> GroupConfigs { get; set; }
    }
}