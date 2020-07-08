using Keycloak.Net.Models.Roles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Keycloak.Net.Models.Groups
{
    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("realmRoles")]
        public IEnumerable<Role> RealmRoles { get; set; }
        [JsonProperty("clientRoles")]
        public IEnumerable<Role> ClientRoles { get; set; }
        [JsonProperty("attributes")]
        public IEnumerable<Attribute> Attributes { get; set; }
        [JsonProperty("subGroups")] 
        public IEnumerable<Group> Subgroups {get; set;}
    }
}
