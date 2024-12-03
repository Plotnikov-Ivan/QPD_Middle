
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace QPD_Middle.Services
{


    public class AddressService : IAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DaDataSettings _daDataSettings;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IHttpClientFactory httpClientFactory, IOptions<DaDataSettings> daDataSettings, ILogger<AddressService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _daDataSettings = daDataSettings.Value;
            _logger = logger;
        }

        public async Task<AddressResponse> StandardizeAddressAsync(string address)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _daDataSettings.BaseUrl)
            {
                Content = new StringContent($"[\"{address}\"]", Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Authorization", $"Token {_daDataSettings.ApiKey}");
            request.Headers.Add("X-Secret", _daDataSettings.Secret);
            request.Headers.Add("Accept", "application/json");

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Response DaData API: {Content}", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("DaData API returned error: {Content}", content);
                throw new HttpRequestException($"DaData API returned error: {content}");
            }

            var dadataResponse = JsonConvert.DeserializeObject<DaDataResponse[]>(content);

            if (dadataResponse == null || dadataResponse.Length == 0)
            {
                _logger.LogError("DaData API returned empty response.");
                throw new InvalidOperationException("DaData API returned empty response.");
            }

            var addressResponse = new AddressResponse
            {
                Country = dadataResponse[0].Country,
                City = dadataResponse[0].City,
                Street = dadataResponse[0].Street,
                HouseNumber = dadataResponse[0].House,
                FlatNumber = dadataResponse[0].Flat
            };

            return addressResponse;
        }
    }
}
