using AutoMapper;
using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Data.Models.Entities;

namespace CryptoCurrencyExchange.Core.Mappings
{
    public class ExchangeRatesProfile : Profile
    {
        public ExchangeRatesProfile()
        {
            CreateMap<ExchangeRates, ExchangeRatesResponse>()
              .ForPath(dest => dest.RatesResponse, opt => opt.MapFrom(src => src.Rates))
              .ReverseMap();
        }
    }
}