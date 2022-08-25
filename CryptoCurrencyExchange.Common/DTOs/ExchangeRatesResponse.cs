using Newtonsoft.Json;

namespace CryptoCurrencyExchange.Common.DTOs
{
    public class ExchangeRatesResponse
    {
        public string Currency { get; set; }

        [JsonProperty("rates")]
        public RatesResponse RatesResponse { get; set; }
    }
}