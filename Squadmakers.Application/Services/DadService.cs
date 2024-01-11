using Newtonsoft.Json;
using Squadmakers.Application.Interfaces;
using Squadmakers.Domain.Dtos;
using System.Net.Mime;

namespace Squadmakers.Application.Services
{
    public class DadService : IJokeService
    {
        private readonly HttpClient _httpClient;

        public DadService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
        }


        public async Task<string?> GetJokeAsync(string? name)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                HttpResponseMessage response = await _httpClient.GetAsync("https://icanhazdadjoke.com/");

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    var dadJoke = JsonConvert.DeserializeObject<DadJoke>(jsonContent);

                    return dadJoke?.Joke;
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
