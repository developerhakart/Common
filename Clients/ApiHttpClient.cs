using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Clients
{

    /// <summary>
    /// https://www.talkingdotnet.com/3-ways-to-use-httpclientfactory-in-asp-net-core-2-1/
    /// </summary>
    public class ApiHttpClient : IApiHttpClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private const string _mediaType = "application/json";

        public ApiHttpClient(HttpClient httpClient, IOptions<ClientSettings> options)
        {
            _baseUrl = options.Value.ApiBaseUrl;
            httpClient.BaseAddress = new Uri(_baseUrl);
            //TODO: move it to clientFactore named vaerion and inject client factory interface
            _client = httpClient;
        }

        
        public async Task<TResponse> Get<TResponse>(string path, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAsync(_baseUrl + path, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(responseAsString);

            return result;

        }

        public async Task<TResponse> Post<TRequest, TResponse>(string path, TRequest model, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(model);
            using (var stringContent = new StringContent(json, Encoding.UTF8, _mediaType))
            {
                var response = await _client.PostAsync(_baseUrl + path, stringContent, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(responseAsString);

                return result;
            }

        }

        public async Task<TResponse> Put<TRequest, TResponse>(string path, TRequest model, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(model);
            using (var stringContent = new StringContent(json, Encoding.UTF8, _mediaType))
            {
                var response = await _client.PutAsync(_baseUrl + path, stringContent, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResponse>(responseAsString);

                return result;
            }
        }

        public async Task<TResponse> Delete<TResponse>(string path, CancellationToken cancellationToken = default)
        {
            var response = await _client.DeleteAsync(_baseUrl + path, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(responseAsString);

            return result;
        }
    }
}
