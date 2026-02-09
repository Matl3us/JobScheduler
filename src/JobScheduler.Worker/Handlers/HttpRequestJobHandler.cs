using System.Net;
using System.Text.Json;
using JobScheduler.Core.DTOs;

namespace JobScheduler.Worker.Handlers;

public class HttpRequestJobHandler(IHttpClientFactory httpClientFactory)
{
    public async Task<(HttpStatusCode, HttpContent)> ExecuteAsync(string jobData)
    {
        try
        {
            var httpData = JsonSerializer.Deserialize<HttpRequestDto>(jobData);
            if (httpData is null) throw new ArgumentException("Job data cannot be null");

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(httpData.Uri),
                Method = new HttpMethod(httpData.Method),
                Content = !string.IsNullOrEmpty(httpData.Body)
                    ? new StringContent(httpData.Body)
                    : null
            };

            foreach (var (key, value)in httpData.Headers) request.Headers.Add(key, value);

            var client = httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            return (response.StatusCode, response.Content);
        }
        catch (JsonException)
        {
            throw new ArgumentException("Job data is not in a valid format");
        }
    }
}