namespace HttpClientUsage
{
    public class HerokuBookingAppClient
    {
        private readonly HttpClient _httpClient;
        private readonly HerokuSettings _herokuSettings;

        //IHttpClientFactory is responsible for creating HttpClient object injected into the constructor.
        public HerokuBookingAppClient(HttpClient httpClient, HerokuSettings herokuSettings)
        {
            _httpClient = httpClient;
            _herokuSettings = herokuSettings;

            //Configure http client here as oppose to configuring it while registering
            _httpClient.BaseAddress = new Uri(herokuSettings.BaseUrl);
        }

        //Like following developers can implement required methods for other endpoints offered by external API.
        public async Task<string> GetBookings()
        {
            var response = await _httpClient.GetAsync("booking");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}