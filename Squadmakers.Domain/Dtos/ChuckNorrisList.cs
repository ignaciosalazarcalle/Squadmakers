using Newtonsoft.Json;

namespace Squadmakers.Domain.Dtos
{
    public class ChuckNorrisList
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("result")]
        public List<ChuckNorrisJoke?> Result { get; set; }
    }
}
