using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;

namespace CryptoCurrencyExchange.Data.Abstractions
{
    public interface IExchangeRateClient
    {
        Task<GenericValidatableResponse<ExchangeRatesResponse>> GetExchangeRates(string currency);
    }
}