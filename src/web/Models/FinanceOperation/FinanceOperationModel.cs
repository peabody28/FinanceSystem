using Newtonsoft.Json;
using System;

namespace web.Models.FinanceOperation
{
    public class FinanceOperationModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("userFk")]
        public Guid UserFk { get; set; }

        [JsonProperty("chainFk")]
        public Guid ChainFk { get; set; }
    }
}
