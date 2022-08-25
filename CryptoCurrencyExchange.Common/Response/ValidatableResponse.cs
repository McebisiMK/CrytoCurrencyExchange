using CryptoCurrencyExchange.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CryptoCurrencyExchange.Common.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ValidatableResponse
    {
        public bool IsValid { get { return !Errors.Any(); } }
        public List<string> Messages { get; set; }
        public List<string> Errors { get; set; }

        public ValidatableResponse()
        {
            Messages = new List<string>();
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            if (error.HasValue())
                Errors.Add(error);
        }

        public void AddMessage(string message)
        {
            if (message.HasValue())
                Messages.Add(message);
        }

        public void MergeResponses(ValidatableResponse validatableResponse)
        {
            if (validatableResponse != null)
            {
                validatableResponse.Errors.ForEach(error =>
                {
                    AddError(error);
                });

                validatableResponse.Messages.ForEach(message =>
                {
                    AddMessage(message);
                });
            }
        }

        public T MergeResponsesAndReturnEntity<T>(GenericValidatableResponse<T> validatableResponse)
        {
            var r = default(T);

            MergeResponses((ValidatableResponse)validatableResponse);

            if (IsValid)
                r = validatableResponse.Data;

            return r;
        }
    }
}