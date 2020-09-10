using Newtonsoft.Json;

namespace Keycloak.Net.Models.Resource
{
    public class Scope
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
