using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Data.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CryptoCurrencyExchange.Data.External
{
    public class ExchangeRateClient : IExchangeRateClient
    {
        public const string CoinbaseApiClientIdentifier = "coinbase";
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ExchangeRateClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GenericValidatableResponse<ExchangeRatesResponse>> GetExchangeRates(string currency)
        {
            var validatableResponse = new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse());

            try
            {
                var client = _httpClientFactory.CreateClient(CoinbaseApiClientIdentifier);
                var coinbaseRelativeUrl = GetCoinbaseRelativeUrl(currency);

                validatableResponse.AddMessage($"Retrieving exchange rates for: {currency}");

                var response = await client.GetAsync(coinbaseRelativeUrl);
                response.EnsureSuccessStatusCode();

                dynamic exchangeRates = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                var data = JsonConvert.SerializeObject(exchangeRates?.data);

                validatableResponse.AddMessage($"Successfully retrieved exchange rates for: {currency}");

                validatableResponse.Data = JsonConvert.DeserializeObject<ExchangeRatesResponse>(data);
            }
            catch (Exception exception)
            {
                var ex = exception.InnerException ?? exception;
                validatableResponse.AddError($"Something happened while retrieving exchange rates. Error: {ex.Message}");
            }

            return validatableResponse;
        }

        private string GetCoinbaseRelativeUrl(string currency)
        {
            var relativeUrl = _configuration.GetValue<string>("coinbaseApiConfig:exchangeRatesRelativeUrl");

            return $"{relativeUrl}?currency={currency}";
        }
    }
}