using Newtonsoft.Json;
using System;

namespace api.Models.FinanceOperationApi
{
    public class FinanceOperationModel
    {
        [JsonProperty("chainId")]
        public Guid ChainId { get; set; }
    }
}
