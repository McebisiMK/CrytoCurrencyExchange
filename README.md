# Cryto Currency Exchange

Api Project (Api entry point)
-
- [Controllers](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Api/Controllers)
	- [CryptoCurrency Controller](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Api/Controllers/CurrencyExchangeRatesController.cs)
		- CryptoCurrency's endpoints (**GetExchangeRates**).
- [appsettings](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Api/appsettings.json)
	- Configurations (**Connection string, Coinbase Api, HttpClient Config**).
- [Program.cs](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Api/Program.cs)
	- Creates WebApplicationBuilder and register services (**AutoMapper, DbContext, DI Resolver, Swagger config**).
	
Common Project (Common functionality of the Application)
-
- [DTOs](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Common/DTOs)
	-  for mapping DB entities.
- [Extensions](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Common/Extensions)
	- String extension methods (**HasValue, IsEmpty**)
- [Response](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Common/Response)
	- Generic Validatable Response
        - **Data:** Contains the actual data returned by the External client (Coinbase) 
        - **IsValid:** Indicates whether the request was success or not.
        - **Errors:** Returns the list of errors thrown by the API.
        - **Messages:** Returns the list of info messages (process steps)
        ```
        {
            "data": {
                "currency": "BTC/ETH",
                "exchangeRates": {
                    ...
                }
            },
            "isValid": true/false,
            "errors": [
                "Error messages"
            ],
            "messages": [
                "Info messages"
            ]
        }
        ```

Core Project (Business logic and object mapping)
-
- [Abstractions](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Core/Abstractions)
	- Service interfaces.
- [Mappings](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Core/Mappings)
	- Object mapping profiles (From Entities to DTO and reverse).
- [Services](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Core/Services)
	- [ExchangeRates Service](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Core/Services/ExchangeRatesService.cs)
		- Business logic for **Retrieving exchange rates from coinbase and adding them to the database**

Data Access Project (ORM and tables)
-
- [Abstractions](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Data/Abstractions)
    -   Repository interface.
    -   External service interface.
 - [Models](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Data/Models)
	 - [Entities](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Data/Models/Entities)
		 - DB Entities (Tables)
	 - [Currency Exchange Rates DbContext](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Data/Models/CurrencyExchangeRatesDbContext.cs)
		 - Entity Framework Core DbContext.
 - [Repositories](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Data/Repositories)
	 - [ExchangeRates repository](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Data/Repositories/ExchangeRateRepository.cs)
        - Logic to store ExchangeRates to the database

Tests Project (Unit test)
-
- [Test Data](https://github.com/McebisiMK/CrytoCurrencyExchange/tree/main/CryptoCurrencyExchange.Tests/TestData)
	- Test data for various test scenarios.
- [Service Tests](https://github.com/McebisiMK/CrytoCurrencyExchange/blob/main/CryptoCurrencyExchange.Tests/CurrencyExchangeRatesServiceTests.cs)
	- Services unit tests.

Tech Used
-
- **[.Net Core 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)**
- **[Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)**
- **[HttpClient Factory](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)**
- **[AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html)**
- **[Swagger UI](https://swagger.io/tools/swagger-ui/)**
- **[NUnit](https://nunit.org/)**
- **[NSubstitute](https://nsubstitute.github.io/help/getting-started/)**
- **[Fluent Assertions](https://fluentassertions.com/introduction)**
- **[GitHub Actions](https://docs.github.com/en/actions)**