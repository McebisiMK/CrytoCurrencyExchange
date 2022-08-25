using AutoMapper;
using CryptoCurrencyExchange.Common.DTOs;
using CryptoCurrencyExchange.Common.Response;
using CryptoCurrencyExchange.Core.Mappings;
using CryptoCurrencyExchange.Core.Services;
using CryptoCurrencyExchange.Data.Abstractions;
using CryptoCurrencyExchange.Data.Models.Entities;
using CryptoCurrencyExchange.Tests.TestData;
using FluentAssertions;
using NSubstitute;

namespace CryptoCurrencyExchange.Tests
{
    [TestFixture]
    public class CurrencyExchangeRatesServiceTests
    {
        private readonly IMapper _mapper;

        public CurrencyExchangeRatesServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mapConfig =>
                {
                    mapConfig.AddProfile(new RatesProfile());
                    mapConfig.AddProfile(new ExchangeRatesProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task GetExchangeRates_Given_Currency_Is_Invalid_Should_Return_Response_With_Errors(string currency)
        {
            //--------------------------------------------Arrange----------------------------------------------
            var currencyClient = Substitute.For<IExchangeRateClient>();
            var currencyRepository = Substitute.For<IExchangeRateRepository>();
            var exchangeRatesService = CreateExchangeRatesService(currencyClient, currencyRepository);

            var expectedResponse = ExchangeRatesServiceResponse.GetInvalidCurrencyErrorResponse();

            //--------------------------------------------Act--------------------------------------------------
            var response = await exchangeRatesService.GetExchangeRates(currency);

            //--------------------------------------------Assert-----------------------------------------------
            response.IsValid.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.Should().BeEquivalentTo(expectedResponse.Data);
            response.Errors.Should().BeEquivalentTo(expectedResponse.Errors);
        }

        [Test]
        public async Task GetExchangeRates_Given_The_HttpClient_Throws_An_Exception_The_Service_Should_Return_Error_Response()
        {
            //--------------------------------------------Arrange----------------------------------------------
            var mapper = Substitute.For<IMapper>();
            var currencyClient = Substitute.For<IExchangeRateClient>();
            var currencyRepository = Substitute.For<IExchangeRateRepository>();
            var exchangeRatesService = CreateExchangeRatesService(currencyClient, currencyRepository);

            var currency = "Invalid";
            var expectedResponse = ExchangeRatesClientResponse.GetInvalidClientResponse();

            currencyClient.GetExchangeRates(currency).Returns(
                Task.FromException<GenericValidatableResponse<ExchangeRatesResponse>>(
                    new Exception("Invalid currency")
                ));
            //--------------------------------------------Act--------------------------------------------------
            var response = await exchangeRatesService.GetExchangeRates(currency);

            //--------------------------------------------Assert-----------------------------------------------
            response.IsValid.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.Should().BeEquivalentTo(expectedResponse.Data);
            response.Errors.Should().BeEquivalentTo(expectedResponse.Errors);
            await currencyRepository.Received(0).Add(Arg.Any<ExchangeRates>());
        }

        [Test]
        public async Task GetExchangeRates_Given_The_HttpClient_Returns_Valid_ExchangeRates_And_DB_Throws_Exception_The_Service_Should_Return_Error_Response()
        {
            //--------------------------------------------Arrange----------------------------------------------
            var mapper = Substitute.For<IMapper>();
            var currencyClient = Substitute.For<IExchangeRateClient>();
            var currencyRepository = Substitute.For<IExchangeRateRepository>();
            var exchangeRatesService = CreateExchangeRatesService(currencyClient, currencyRepository);

            var currency = "BTC";
            var expectedResponse = ExchangeRatesServiceResponse.GetResponseWithInvalidRepositoryResponse();

            currencyClient.GetExchangeRates(currency).Returns(ExchangeRatesClientResponse.GetExchangeRates());
            currencyRepository.Add(Arg.Any<ExchangeRates>()).Returns(
                Task.FromException<ValidatableResponse>(
                    new Exception("Invalid request")
                ));
            //--------------------------------------------Act--------------------------------------------------
            var response = await exchangeRatesService.GetExchangeRates(currency);

            //--------------------------------------------Assert-----------------------------------------------
            response.IsValid.Should().BeFalse();
            response.Data.Should().BeEquivalentTo(expectedResponse.Data);
            response.Errors.Should().BeEquivalentTo(expectedResponse.Errors);
            response.Messages.Should().BeEquivalentTo(expectedResponse.Messages);
            await currencyRepository.Received(1).Add(Arg.Any<ExchangeRates>());
        }

        [Test]
        public async Task GetExchangeRates_Given_The_HttpClient_Returns_Valid_ExchangeRates_The_Service_Should_Call_The_Database_To_Add_ExchangeRates()
        {
            //--------------------------------------------Arrange----------------------------------------------
            var mapper = Substitute.For<IMapper>();
            var currencyClient = Substitute.For<IExchangeRateClient>();
            var currencyRepository = Substitute.For<IExchangeRateRepository>();
            var exchangeRatesService = CreateExchangeRatesService(currencyClient, currencyRepository);

            var currency = "BTC";
            var expectedResponse = ExchangeRatesServiceResponse.GetResponseWithValidRepositoryResponse();

            currencyClient.GetExchangeRates(currency).Returns(ExchangeRatesClientResponse.GetExchangeRates());
            currencyRepository.Add(Arg.Any<ExchangeRates>()).Returns(ExchangeRatesRepositoryResponse.GetValidRepositoryResponse());

            //--------------------------------------------Act--------------------------------------------------
            var response = await exchangeRatesService.GetExchangeRates(currency);

            //--------------------------------------------Assert-----------------------------------------------
            response.IsValid.Should().BeTrue();
            response.Data.Should().BeEquivalentTo(expectedResponse.Data);
            response.Errors.Should().BeEquivalentTo(expectedResponse.Errors);
            response.Messages.Should().BeEquivalentTo(expectedResponse.Messages);
            await currencyRepository.Received(1).Add(Arg.Any<ExchangeRates>());
        }

        private ExchangeRatesService CreateExchangeRatesService(IExchangeRateClient currencyClient, IExchangeRateRepository currencyRepository)
        {
            return new ExchangeRatesService(_mapper, currencyClient, currencyRepository);
        }
    }
}