using Newtonsoft.Json;
using Squadmakers.Application.Interfaces;
using Squadmakers.Domain.Dtos;

namespace Squadmakers.Application.Services
{
    public class ChuckNorrisService : IJokeService
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
        }

        public async Task<string?> GetJokeAsync(string? name)
        {
            try
            {
                var url = "https://api.chucknorris.io/";
                var joke = "jokes/random";
                if (!string.IsNullOrWhiteSpace(name))
                {
                    joke = $"jokes/search?query={name}";
                }

                HttpResponseMessage response = await _httpClient.GetAsync($"{url}{joke}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    ChuckNorrisJoke? chuckNorrisJoke = null;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var chuckNorrisList = JsonConvert.DeserializeObject<ChuckNorrisList>(jsonContent);
                        if (chuckNorrisList is { Total: > 0 })
                        {
                            chuckNorrisJoke = chuckNorrisList.Result.First();
                        }
                    }
                    else
                    {
                        chuckNorrisJoke = JsonConvert.DeserializeObject<ChuckNorrisJoke>(jsonContent);
                    }

                    return chuckNorrisJoke?.Value;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
