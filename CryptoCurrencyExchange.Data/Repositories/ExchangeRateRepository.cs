using System.Linq.Expressions;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Data.Abstractions;
using CryptoCurrencyExchange.Data.Models;
using CryptoCurrencyExchange.Data.Models.Entities;

namespace CryptoCurrencyExchange.Data.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly CurrencyExchangeRatesDbContext _currencyExchangeDbContext;

        public ExchangeRateRepository(CurrencyExchangeRatesDbContext currencyExchangeDbContext)
        {
            _currencyExchangeDbContext = currencyExchangeDbContext;
        }

        public async Task<ValidatableResponse> Add(ExchangeRates exchangeRates)
        {
            var response = new ValidatableResponse();

            try
            {
                response.AddMessage($"Adding {exchangeRates.Currency} exchange rates to the database");

                await _currencyExchangeDbContext.AddAsync(exchangeRates);
                await _currencyExchangeDbContext.SaveChangesAsync();

                response.AddMessage($"Added {exchangeRates.Currency} rates to the database");
            }
            catch (Exception exception)
            {
                var ex = exception.InnerException ?? exception;
                response.AddError($"Something happened while adding exchange rates to the database. Error: {ex.Message}");
            }

            return response;
        }

        public bool Exists(Expression<Func<ExchangeRates, bool>> expression)
        {
            return _currencyExchangeDbContext.ExchangeRates.Any(expression);
        }
    }
}