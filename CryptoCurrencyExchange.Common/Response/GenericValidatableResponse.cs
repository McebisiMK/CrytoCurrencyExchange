using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CryptoCurrencyExchange.Common.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GenericValidatableResponse<T> : ValidatableResponse
    {
        public T Data { get; set; }

        public GenericValidatableResponse(T data)
        {
            Data = data;
        }
    }
}