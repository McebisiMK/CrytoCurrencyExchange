using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;

namespace CryptoCurrencyExchange.Tests.TestData
{
    public class ExchangeRatesClientResponse
    {
        public static GenericValidatableResponse<ExchangeRatesResponse> GetExchangeRates()
        {
            return new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse())
            {
                Data = new ExchangeRatesResponse
                {
                    Currency = "BTC",
                    RatesResponse = new RatesResponse
                    {
                        AED = "AED",
                        AFN = "AFN",
                        ALL = "ALL",
                        AMD = "AMD",
                        ANG = "ANG",
                        AOA = "AOA",
                        ARS = "ARS",
                        AWG = "AWG",
                        AZN = "AZN"
                    }
                },
                Messages = new List<string>
                {
                    "Retrieving exchange rates for: BTC",
                    "Successfully retrieved exchange rates for: BTC"
                }
            };
        }

        public static GenericValidatableResponse<ExchangeRatesResponse> GetInvalidClientResponse()
        {
            return new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse())
            {
                Errors = new List<string> { "Something happened while retrieving exchange rates. Error: Invalid currency" }
            };
        }
    }
}