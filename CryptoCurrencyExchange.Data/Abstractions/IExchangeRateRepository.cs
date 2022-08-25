using System.Linq.Expressions;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Data.Models.Entities;

namespace CryptoCurrencyExchange.Data.Abstractions
{
    public interface IExchangeRateRepository
    {
        Task<ValidatableResponse> Add(ExchangeRates exchangeRates);
        bool Exists(Expression<Func<ExchangeRates, bool>> expression);
    }
}