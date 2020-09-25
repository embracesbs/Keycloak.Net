using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Clients
{
    public class AuthorizationPermission
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonConverter(typeof(AuthorizationPermissionTypeConverter))]
        public AuthorizationPermissionType Type { get; set; }
        [JsonConverter(typeof(PolicyDecisionLogicConverter))]
        public PolicyDecisionLogic Logic { get; set; } 
        [JsonConverter(typeof(DecisionStrategiesConverter))]
        public DecisionStrategy DecisionStrategy { get; set; }
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; } //only in type=resource
    }

    public enum PolicyDecisionLogic
    {
        Positive, 
        Negative
    }

    public enum AuthorizationPermissionType
    {   
        Scope, 
        Resource
    }

    public enum DecisionStrategy
    {
        Unanimous, 
        Affirmative, 
        Consensus
    }
}