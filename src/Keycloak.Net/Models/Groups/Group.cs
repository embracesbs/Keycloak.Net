using Keycloak.Net.Models.Roles;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Keycloak.Net.Models.Groups
{
    public class Simplegroup
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("subGroups")]
        public IEnumerable<Simplegroup> Subgroups { get; set; }
    }

    public class Group : Simplegroup
    {
        [JsonProperty("realmRoles")]
        public IEnumerable<Role> RealmRoles { get; set; }
        [JsonProperty("clientRoles")]
        public IDictionary<string, string> ClientRoles { get; set; }
        [JsonProperty("attributes")]
        public IDictionary<string, string> Attributes { get; set; }
    }
}
