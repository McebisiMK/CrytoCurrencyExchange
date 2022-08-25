using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;

namespace CryptoCurrencyExchange.Core.Abstractions
{
    public interface IExchangeRatesService
    {
        Task<GenericValidatableResponse<ExchangeRatesResponse>> GetExchangeRates(string currency);
    }
}