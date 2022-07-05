using Saphyre.Client.ViewModels;
using System.Text;
using System.Text.Json;

namespace Saphyre.Client.Providers
{
    public class SaphyreUsersProvider : ISaphyreUsersProvider
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public SaphyreUsersProvider(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task CreateSaphyreUser(SaphyreUser saphyreUser)
        {
            var body = new StringContent(JsonSerializer.Serialize(saphyreUser), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("saphyreusers", body);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
        }

        public async Task<List<SaphyreUser>> GetSaphyreUsers()
        {
            var response = await _client.GetAsync("saphyreusers");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var saphyreUsers = JsonSerializer.Deserialize<List<SaphyreUser>>(content, _options);
            return saphyreUsers;
        }

        public async Task<SaphyreUser> GetSaphyreUser(string? userId)
        {
            var response = await _client.GetAsync($"saphyreusers/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var saphyreUser = JsonSerializer.Deserialize<SaphyreUser>(content, _options);
            return saphyreUser;
        }

        public async Task UpdateSaphyreUser(SaphyreUser saphyreUser)
        {
            var body = new StringContent(JsonSerializer.Serialize(saphyreUser), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"saphyreusers/{saphyreUser.UserId}", body);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
        }

        public async Task DeleteSaphyreUser(int userId)
        {
            var response = await _client.DeleteAsync($"saphyreusers/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
        }
    }
}
