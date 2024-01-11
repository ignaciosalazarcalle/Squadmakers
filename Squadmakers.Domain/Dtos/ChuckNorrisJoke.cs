using Newtonsoft.Json;

namespace Squadmakers.Domain.Dtos
{
    public class ChuckNorrisJoke
    {
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}
