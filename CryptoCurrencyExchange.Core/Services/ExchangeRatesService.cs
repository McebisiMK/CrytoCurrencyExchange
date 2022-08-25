using AutoMapper;
using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Extensions;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Core.Abstractions;
using CryptoCurrencyExchange.Data.Abstractions;
using CryptoCurrencyExchange.Data.Models.Entities;

namespace CryptoCurrencyExchange.Core.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IMapper _mapper;
        private readonly IExchangeRateClient _currencyExchangeRateClient;
        private readonly IExchangeRateRepository _currencyExchangeRateRepository;

        public ExchangeRatesService
        (
            IMapper mapper,
            IExchangeRateClient currencyExchangeRateClient,
            IExchangeRateRepository currencyExchangeRateRepository
        )
        {
            _mapper = mapper;
            _currencyExchangeRateClient = currencyExchangeRateClient;
            _currencyExchangeRateRepository = currencyExchangeRateRepository;
        }

        public async Task<GenericValidatableResponse<ExchangeRatesResponse>> GetExchangeRates(string currency)
        {
            var response = new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse());

            if (currency.IsEmpty())
            {
                response.AddError("Currency cannot be null orr empty");
                return response;
            }

            try
            {
                var externalClientResponse = await _currencyExchangeRateClient.GetExchangeRates(currency);
                response.Data = response.MergeResponsesAndReturnEntity(externalClientResponse);

                if (response.IsValid)
                {
                    var databaseResponse = await AddExchangeRatesToDatabase(response.Data);
                    response.MergeResponses(databaseResponse);
                }
            }
            catch (Exception exception)
            {
                var ex = exception.InnerException ?? exception;
                response.AddError($"Something happened while retrieving exchange rates. Error: {ex.Message}");
            }

            return response;
        }

        private async Task<ValidatableResponse> AddExchangeRatesToDatabase(ExchangeRatesResponse exchangeRatesResponse)
        {
            var response = new ValidatableResponse();

            try
            {
                response.AddMessage("Mapping Client response to Database entity");

                var exchangeRates = _mapper.Map<ExchangeRates>(exchangeRatesResponse);

                response.AddMessage("Mapped Client response to Database entity");

                var databaseResponse = await _currencyExchangeRateRepository.Add(exchangeRates);
                response.MergeResponses(databaseResponse);
            }
            catch (Exception exception)
            {
                var ex = exception.InnerException ?? exception;
                response.AddError($"Something happened while adding exchange rates to the database. Error: {ex.Message}");
            }

            return response;
        }
    }
}