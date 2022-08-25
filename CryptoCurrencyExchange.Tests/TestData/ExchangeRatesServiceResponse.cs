using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;

namespace CryptoCurrencyExchange.Tests.TestData
{
    public class ExchangeRatesServiceResponse
    {
        public static GenericValidatableResponse<ExchangeRatesResponse> GetResponseWithInvalidRepositoryResponse()
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
                Errors = new List<string> { "Something happened while adding exchange rates to the database. Error: Invalid request" },
                Messages = new List<string>
                {
                    "Retrieving exchange rates for: BTC",
                    "Successfully retrieved exchange rates for: BTC",
                    "Mapping Client response to Database entity",
                    "Mapped Client response to Database entity"
                }
            };
        }

        public static GenericValidatableResponse<ExchangeRatesResponse> GetResponseWithValidRepositoryResponse()
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
                    "Successfully retrieved exchange rates for: BTC",
                    "Mapping Client response to Database entity",
                    "Mapped Client response to Database entity",
                    "Adding BTC exchange rates to the database",
                    "Added BTC rates to the database"
                }
            };
        }

        public static GenericValidatableResponse<ExchangeRatesResponse> GetInvalidCurrencyErrorResponse()
        {
            return new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse())
            {
                Errors = new List<string> { "Currency cannot be null orr empty" }
            };
        }
    }
}