using Newtonsoft.Json;

namespace Prueba_Vecttor_Nasa.Models.APIModels
{
    public class Links
    {
        [JsonProperty("next")]
        public Uri? Next { get; set; }

        [JsonProperty("previous")]
        public Uri? Previous { get; set; }

        [JsonProperty("self")]
        public Uri? Self { get; set; }
    }
}
