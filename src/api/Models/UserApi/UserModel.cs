using Newtonsoft.Json;

namespace api.Models.UserApi
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
