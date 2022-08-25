using CryptoCurrencyExchange.Common.Response;

namespace CryptoCurrencyExchange.Tests.TestData
{
    public class ExchangeRatesRepositoryResponse
    {
        public static ValidatableResponse GetValidRepositoryResponse()
        {
            return new ValidatableResponse
            {
                Messages = new List<string>
                {
                    "Adding BTC exchange rates to the database",
                    "Added BTC rates to the database"
                }
            };
        }
    }
}