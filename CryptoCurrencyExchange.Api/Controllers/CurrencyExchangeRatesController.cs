using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyExchange.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeRatesController : Controller
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public CurrencyExchangeRatesController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet("GetByCurrency/{currency}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<GenericValidatableResponse<ExchangeRatesResponse>> GetExchangeRates(string currency)
        {
            var response = new GenericValidatableResponse<ExchangeRatesResponse>(new ExchangeRatesResponse());

            var serviceResponse = await _exchangeRatesService.GetExchangeRates(currency);
            response.Data = response.MergeResponsesAndReturnEntity(serviceResponse);

            return response;
        }
    }
}