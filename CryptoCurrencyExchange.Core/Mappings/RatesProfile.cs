using AutoMapper;
using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Data.Models.Entities;

namespace CryptoCurrencyExchange.Core.Mappings
{
    public class RatesProfile : Profile
    {
        public RatesProfile()
        {
            CreateMap<Rates, RatesResponse>()
              .ReverseMap();
        }
    }
}