using Newtonsoft.Json;
using System.Collections.Generic;

namespace Keycloak.Net.Models.Resource
{
    public class Resource
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("scopes")]
        public IEnumerable<Scope> Scopes { get; set; }
        [JsonProperty("attributes")]
        public Dictionary<string, string> Attributes { get; set; }
        [JsonProperty("uris")]
        public IEnumerable<string> Uris { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ownerManagedAccess")]
        public bool? OwnerManagedAccess { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; } 
    }
}
