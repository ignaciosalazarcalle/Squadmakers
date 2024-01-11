using Newtonsoft.Json;

namespace Squadmakers.Domain.Dtos
{
    public class DadJoke
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
