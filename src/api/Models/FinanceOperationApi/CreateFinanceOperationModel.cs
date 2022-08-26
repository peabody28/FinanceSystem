using Newtonsoft.Json;

namespace api.Models.FinanceOperationApi
{
    public class CreateFinanceOperationModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
