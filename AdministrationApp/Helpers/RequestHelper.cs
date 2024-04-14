using System.Net.Http;
using System.Text.Json;

namespace AdministrationApp.Helpers
{
    public class RequestHelper
    {
        public const string API_URL = @"https://localhost:7280";

        public static async Task<ResponseType> SendRequestAsync<RequestType, ResponseType>(string url, HttpMethod method, RequestType? request, string? token)
        {
            HttpClient client = new()
            {
                BaseAddress = new(API_URL)
            };
            HttpResponseMessage response;
            HttpRequestMessage message = new(method, url);
            if (token != null)
            {
                message.Headers.Authorization = new("Bearer", token);
            }
            if (method != HttpMethod.Get && request != null)
            {
                message.Content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
            }
            response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<ResponseType>(responseString) ?? throw new ArgumentException(responseString);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException(responseString);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException(responseString);
            }
            throw new Exception(responseString);
        }

        public static async Task SendRequestAsync<RequestType>(string url, HttpMethod method, RequestType? request, string? token)
        {
            HttpClient client = new()
            {
                BaseAddress = new(API_URL)
            };
            HttpResponseMessage response;
            HttpRequestMessage message = new(method, url);
            if (token != null)
            {
                message.Headers.Authorization = new("Bearer", token);
            }
            if (method != HttpMethod.Get)
            {
                message.Content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
            }
            response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(responseString);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(responseString);
                }
                throw new Exception(responseString);
            }
        }

        public static async Task SendRequestAsync(string url, HttpMethod method, string? token)
        {
            HttpClient client = new()
            {
                BaseAddress = new(API_URL)
            };
            HttpResponseMessage response;
            HttpRequestMessage message = new(method, url);
            if (token != null)
            {
                message.Headers.Authorization = new("Bearer", token);
            }
            response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(responseString);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(responseString);
                }
                throw new Exception(responseString);
            }
        }
    }
}
