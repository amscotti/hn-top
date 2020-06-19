using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace hn_top
{
    class StoryService
    {

        private const String BaseAPI = "https://hacker-news.firebaseio.com/v0";

        private readonly JsonSerializerOptions options;
        private readonly HttpClient client;

        public StoryService()
        {
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            client = new HttpClient();
        }

        public async Task<int[]> GetTopStories()
        {
            var response = await client.GetAsync($"{BaseAPI}/topstories.json");
            string stories = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<int[]>(stories);
        }

        public async Task<Story> GetStory(int id)
        {
            var response = await client.GetAsync($"{BaseAPI}/item/{id}.json");
            string story = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Story>(story, options);
        }

    }
}
