using Newtonsoft.Json;

namespace api.Models.LoginApi
{
    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
