using Newtonsoft.Json;

namespace web.Models.SignUp
{
    public class RedirectModel
    {
        [JsonProperty("url")]
        public string Url { get; set;}
    }
}
